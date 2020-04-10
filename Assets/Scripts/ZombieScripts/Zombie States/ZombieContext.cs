using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]

public abstract class ZombieContext : Context<ZombieContext>, IDamageAble
{
    #region Fields

    #region Zombie States

    public ZombieIdleState idleState = new ZombieIdleState();
    public ZombiePatrollingState patrolState = new ZombiePatrollingState();
    public ZombieInvestigateState investigateState = new ZombieInvestigateState();
    public ZombieChasingState chaseState = new ZombieChasingState();
    public ZombieAttackingState attackState = new ZombieAttackingState();
    public ZombieDeadState deadState = new ZombieDeadState();
    public ZombieDespawnState despawnState = new ZombieDespawnState();

    #endregion

    public Animator zombieAnimator;

    // Movement variables
    public NavMeshAgent zombieNavMeshAgent;
    public float epsilon = 0.1f;
    public float walkSpeed;
    public float minimumRunSpeed;
    public float maximumRunSpeed;
    public float runSpeed;
    public float currentSpeed;

    // Health generation variables
    public float minimumHealth;
    public float maximumHealth;
    public float health;

    // Damage generation variables
    public float minimumDamage;
    public float maximumDamage;
    public float damage;

    // Calculation distance values
    public float fieldOfView;
    public float visionDistance;
    public float deadDespawnDistance;
    public float livingDespawnDistance;

    // Idle time range
    public float minimumIdleTime;
    public float maximumIdleTime;

    // Track player and current target position
    public Transform playerTransform;
    public Vector3 currentTarget;

    public bool playerDead = false;
    public bool heardSound = false;
    public Vector3 soundLocation;
    
    // Sound effects
    public AudioClip[] idleSounds;
    public AudioClip[] attackSounds;
    public AudioClip[] hurtSounds;
    public AudioClip[] deathSounds;

    public float minTimeBetweenSounds;
    public float maxTimeBetweenSounds;
    public float nextSoundTime;

    public AudioSource audioSource;

    #endregion

    public override void InitializeContext()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        zombieNavMeshAgent.enabled = true;
        if (!zombieNavMeshAgent.isOnNavMesh)
        {
            currentState = despawnState;
            currentState.EnterState(this);
            return;
        }

        DisableRagdoll();

        EventManager.PlayerKilled += PlayerKilled;

        playerTransform = PlayerManager.instance.player.transform;
        currentSpeed = walkSpeed;
        runSpeed = Random.Range(minimumRunSpeed, maximumRunSpeed);
        health = Random.Range(minimumHealth, maximumHealth);
        damage = Random.Range(minimumDamage, maximumDamage);
        
        nextSoundTime = Time.time + Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);

        EventManager.SoundGenerated += SoundGenerated;

        currentState = idleState;
        idleState.EnterState(this);
    }

    public void Damage(float damage)
    {
        if (currentState == deadState) return;

        health -= damage;

        if (health <= 0){
            Debug.LogWarning("Played death sound.");
            this.PlaySound(deathSounds);
            currentState = deadState;
        }
        else{
            Debug.LogWarning("Played hurt sound.");
            this.PlaySound(hurtSounds);
            currentState = chaseState;
        }

        currentState.EnterState(this);
    }

    public void DisableRagdoll()
    {
        Rigidbody[] rigidBodies = this.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
            rigidBody.useGravity = false;
        }
    }

    public void EnableRagdoll()
    {
        zombieAnimator.enabled = false;
        zombieNavMeshAgent.enabled = false;

        Rigidbody[] rigidBodies = this.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
        }
    }

    private void PlayerKilled()
    {
        playerDead = true;
    }

    public void SoundGenerated(Vector3 location, float audibleDistance)
    {
        if (Vector3.Distance(this.transform.position, location) > audibleDistance) return;

        heardSound = true;
        soundLocation = location;
    }
    
    public void PlayTimedSound(ZombieBaseState state)
    {
        float distance = Vector3.Distance(this.transform.position, this.playerTransform.position);
        if (distance > this.visionDistance) return;

        if (Time.time < this.nextSoundTime) return;

        float pauseScale = 1.0F;

        if (state == this.idleState || state == this.patrolState || state == this.investigateState)
        {
            Debug.LogWarning("Played idle sound.");
            PlaySound(this.idleSounds);
            pauseScale = 2.5F;
        }
        else if(state == this.chaseState)
        {
            Debug.LogWarning("Played chase sound.");
            PlaySound(this.attackSounds);
        }

        this.nextSoundTime = Time.time + Random.Range(this.minTimeBetweenSounds, this.maxTimeBetweenSounds) * pauseScale;
    }

    public void PlaySound(AudioClip[] clipArray){
        int index = Random.Range(0, clipArray.Length);
        AudioClip clipToPlay = clipArray[index];

        AudioSource.PlayClipAtPoint(clipToPlay, this.transform.position);
    }
}