using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    [SerializeField] LayerMask obstacleMask;

    enum State { IDLE, PATROL, ALERT, CHASE, ATTACK, HIT, DIE }
    [SerializeField] State currentState = State.IDLE;

    [SerializeField] GameObject player;
    [SerializeField] float hearDistance;
    [SerializeField] float fieldOfViewAngle = 45.0f;

    [Header("IDLE")]
    float idleStarted = 0.0f;
    [SerializeField] float idleDuration = 5f;

    [Header("PATROL")]
    [SerializeField] List<Transform> patrolTargets;
    [SerializeField] float patrolMinDistance;
    [SerializeField] int patrolRoundsToIdle;
    [SerializeField] float patrolSpeed;
    [SerializeField] float patrolAcceleration;
    int currentPatrolTarget = 0;

    [Header("ALERT")]
    [SerializeField] float alertSpeedRotation;
    [SerializeField]float totalRotated = 0.0f;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.ATTACK:
                //updateAttacking();
                //changeFromAttacking();
                break;
            case State.IDLE:
                updateIdle();
                changeFromIdle();
                break;
            case State.PATROL:
                updatePatrol();
                changeFromPatrol();
                break;
            case State.ALERT:
                updateAlert();
                changeFromAlert();
                break;
        }
        //agent.SetDestination(target.position);
    }

    void updateIdle()
    {
        // Do nothing
    }

    void changeFromIdle()
    {
        if (Time.time >= idleStarted + idleDuration)
        {
            currentState = State.PATROL;
            currentPatrolTarget = 0;
        }
    }

    void updatePatrol()
    {
        if (agent.isStopped) agent.isStopped = false;
        agent.speed = patrolSpeed;
        agent.acceleration = patrolAcceleration;
        if (agent.hasPath && agent.remainingDistance < patrolMinDistance)
        {
            currentPatrolTarget++;
        }
        agent.SetDestination(patrolTargets[currentPatrolTarget % patrolTargets.Count].position);
    }

    void changeFromPatrol()
    {
        if(currentPatrolTarget > patrolRoundsToIdle * patrolTargets.Count)
        {
            currentState = State.IDLE;
            idleStarted = Time.time;
        }
        if (hearsPlayer())
        {
            currentState = State.ALERT;
            totalRotated = 0.0f;
        }
    }

    bool hearsPlayer()
    {
        return (transform.position - player.transform.position).magnitude < hearDistance;
    }

    void updateAlert()
    {
        agent.isStopped = true;
        float frameRotation = alertSpeedRotation * Time.deltaTime;
        transform.Rotate(new Vector3(0.0f, alertSpeedRotation * Time.deltaTime, 0.0f));
        totalRotated += frameRotation;
    }

    void changeFromAlert()
    {
        if (seesPlayer())
        {
            Debug.Log("SEEEEEEESSS");
            currentState = State.CHASE;
        }
        if (!hearsPlayer() || totalRotated >= 360.0f)
        {
            currentState = State.IDLE;
            idleStarted = Time.time;
        }
    }

    bool seesPlayer()
    {
        //if (!hearsPlayer()) return false;
        //return noObstacleBetweenPlayer() && playerInFieldOfView();   


        Ray r = new Ray(transform.position, player.transform.position - transform.position);
        float playerDist = (player.transform.position - transform.position).magnitude;
        if (Physics.Raycast(r, out RaycastHit hitInfo, playerDist, obstacleMask))
        {
            return false;
        }
        return true;
    }

    /* bool noObstacleBetweenPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
        {
            if (hit.collider.gameObject == player)
            {
                return true;
            }
        }
        return false;
    }

    bool playerInFieldOfView()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        return angleToPlayer < fieldOfViewAngle;
    } */

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
