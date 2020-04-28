using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceStationNPCController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private int idleAnimationInt; //MUST be 0 if waypoints is not empty;
    [SerializeField] private bool shouldWalk; //MUST Be false if waypoints is empty
    [SerializeField] private Transform[] waypoints; // leave empty if you dont want NPC to walk
    private float minWalkDelay = 2f;
    private float maxWalkDelay = 6f;
    private float timeToWalk;
    [SerializeField] private Transform retreatPoint; //where they should go for mission 5
    private bool shouldRetreat = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator.SetInteger("Animation_int", idleAnimationInt);
        navMeshAgent.speed = 2f;
        animator.SetFloat("Speed_f", 0f);
        timeToWalk = Time.time + Random.Range(minWalkDelay, maxWalkDelay);
        EventManager.DisableFloodLightSounds += DisableFloodLightSounds; //really shouldn't use this event, but its called when the lights go out in mission 5, so it works.
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length != 0 && !shouldRetreat)
        {
            if (shouldWalk && timeToWalk < Time.time)
            {
                shouldWalk = false;
                animator.SetFloat("Speed_f", 0.49f);
                StartCoroutine(NavMeshStartDelay());
            }
            if (!shouldWalk && navMeshAgent.remainingDistance <= 0.5f)
            {
                shouldWalk = true;
                animator.SetFloat("Speed_f", 0f);
                timeToWalk = Time.time + Random.Range(minWalkDelay, maxWalkDelay);
            }
        }
    }

    private void DisableFloodLightSounds()
    {
        animator.SetFloat("Speed_f", 0.9f);
        animator.SetInteger("Animation_int", 0);
        navMeshAgent.speed = 5f;
        shouldRetreat = true;
        StartCoroutine(RetreatNavMesh());
    }

    IEnumerator RetreatNavMesh()
    {
        navMeshAgent.SetDestination(retreatPoint.position);
        yield return new WaitForSeconds(3f);
        navMeshAgent.SetDestination(retreatPoint.position);
        yield return new WaitForSeconds(3f);
        navMeshAgent.SetDestination(retreatPoint.position);
    }

    IEnumerator NavMeshStartDelay()
    {
        yield return new WaitForSeconds(3.25f);
        navMeshAgent.SetDestination(waypoints[Random.Range(0, waypoints.Length)].position);
    }
}
