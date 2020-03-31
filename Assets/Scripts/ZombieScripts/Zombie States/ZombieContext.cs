using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ZombieContext : Context<ZombieContext>, IDamageAble
{
    #region Fields

    #region Zombie States

    public ZombieIdleState idleState = new ZombieIdleState();
    public ZombiePatrollingState patrolState = new ZombiePatrollingState();
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
    public float hearingDistance;
    public float deadDespawnDistance;
    public float livingDespawnDistance;

    // Idle time range
    public float minimumIdleTime;
    public float maximumIdleTime;

    // Track player and current target position
    public Transform playerTransform;
    public Vector3 currentTarget;

    public bool playerDead;

    #endregion

    public override void InitializeContext()
    {
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

        currentState = idleState;
        idleState.EnterState(this);
    }

    public void Damage(float damage)
    {
        if (currentState == deadState) return;

        health -= damage;

        if (health <= 0) currentState = deadState;

        else currentState = chaseState;

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
}