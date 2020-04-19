using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimFSM : Context<PlayerAnimFSM>, IDamageAble
{
    public PlayerAnimIdle idleState = new PlayerAnimIdle();
    public PlayerAnimJump jumpState = new PlayerAnimJump();
    public PlayerAnimRun runState = new PlayerAnimRun();
    public PlayerAnimWalk walkState = new PlayerAnimWalk();
    public PlayerAnimDead deadState = new PlayerAnimDead();
    public PlayerAnimCrouch crouchState = new PlayerAnimCrouch();

    [HideInInspector]
    public Animator playerAnimator;
    private Camera playerCam;
    private float cameraAngle;
    public float health;
    public WeaponContext weaponContext;


    // Start is called before the first frame update
    public override void InitializeContext()
    {
        EventManager.TriggerPlayerHealthChanged(health);

        playerAnimator = GetComponentInChildren<Animator>();
        playerCam = GetComponentInChildren<Camera>();
        currentState = idleState;
        currentState.EnterState(this);

        EventManager.PlayerDamaged += Damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeContext();
    }

    // Update is called once per frame
    void Update()
    {
        cameraAngle = playerCam.transform.rotation.eulerAngles.x;
        cameraAngle = (cameraAngle > 180) ? (cameraAngle - 360) / 90f : cameraAngle / 90f;
        playerAnimator.SetFloat("Body_Vertical_f", -cameraAngle);
        ManageState(this);
    }

    public void Damage(float damage)
    {
        if (currentState == deadState) return;

        health = Mathf.Clamp((health - damage), 0f, 100f);

        EventManager.TriggerPlayerHealthChanged(health);

        if (health == 0f)
        {
            currentState = deadState;
            currentState.EnterState(this);
        }
    }
}
