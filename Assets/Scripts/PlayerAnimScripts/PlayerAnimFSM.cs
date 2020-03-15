using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimFSM : MonoBehaviour
{

    private PlayerAnimBase currentState;
    public readonly PlayerAnimIdle idleState = new PlayerAnimIdle();
    public readonly PlayerAnimJump jumpState = new PlayerAnimJump();
    public readonly PlayerAnimRun runState = new PlayerAnimRun();
    public readonly PlayerAnimWalk walkState = new PlayerAnimWalk();
    public readonly PlayerAnimDead deadState = new PlayerAnimDead();

    [HideInInspector]
    public Animator playerAnimator;
    private Camera playerCam;
    private float cameraAngle;
    public float health;
    public WeaponContext weaponContext;


    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        playerCam = GetComponentInChildren<Camera>();
        TransitionToState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        cameraAngle = playerCam.transform.rotation.eulerAngles.x;
        cameraAngle = (cameraAngle > 180) ? (cameraAngle - 360)/90f : cameraAngle/90f;
        playerAnimator.SetFloat("Body_Vertical_f", -cameraAngle);

        currentState.Update(this);
    }

    public void TransitionToState(PlayerAnimBase state)
    {
        currentState = state;
        currentState.EnterState(this);
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
