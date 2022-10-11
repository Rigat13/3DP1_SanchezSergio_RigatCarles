using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;

    enum State { IDLE, ATTACKING }
    State currentState = State.IDLE;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.ATTACKING:
                updateAttacking();
                changeFromAttacking();
                break;
            case State.IDLE:
                updateIdle();
                changeFromIdle();
                break;
        }
        //agent.SetDestination(target.position);
    }

    void updateAttacking()
    {
        agent.SetDestination(target.position);
    }

    void changeFromAttacking()
    {
        if (Vector3.Distance(transform.position, target.position) > 2.0f)
        {
            currentState = State.IDLE;
        }
    }
}
