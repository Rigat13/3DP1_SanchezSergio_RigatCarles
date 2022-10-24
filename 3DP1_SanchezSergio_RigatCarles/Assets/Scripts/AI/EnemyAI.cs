using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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


    [Header("DIE")]
    Material mat;
    [SerializeField] ParticleSystem explosion;

    [Header("ATTACK")]
    [SerializeField] float attackRange;
    [SerializeField] float damage;
    [SerializeField] ParticleSystem flash;
    [SerializeField] float invulnerabilityDuration;
    float invulnerabilityStarted;

    [Header("CHASE")]
    [SerializeField] float chaseRange;
    [SerializeField] float chaseSpeed;

    [Header("HIT")]
    State lastState;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sound_hit;
    [SerializeField] AudioClip sound_shoot;

    [Header("UI")]
    [SerializeField] Slider HPSlider;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mat = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.ATTACK:
                updateAttacking();
                changeFromAttacking();
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
            case State.CHASE:
                updateChase();
                changeFromChase();
                break;
            case State.HIT:
                changeFromHit();
                break;
            case State.DIE:
                updateDie();
                break;
        }
        HealthSystem hs = gameObject.GetComponent<HealthSystem>();
        HPSlider.value = hs.currentHealth / hs.maxHealth;
    }

    public void getHit()
    {
        
        lastState = currentState;
        currentState = State.HIT;
        
    }

    private void changeFromHit()
    {
        HealthSystem hs = gameObject.GetComponent<HealthSystem>();
        if (hs.currentHealth <= 0)
        {
            currentState = State.DIE;
        }
        else
        {
            if (lastState == State.PATROL || lastState == State.IDLE)
            {
                currentState = State.ALERT;
            }
            else
            {
                currentState = lastState;
            }
        }
    }

    private void changeFromChase()
    {
        agent.speed = chaseSpeed;
        this.transform.LookAt(player.transform);
        if((transform.position - player.transform.position).magnitude < attackRange)
        {
            currentState = State.ATTACK;
        }
        if ((transform.position - player.transform.position).magnitude > chaseRange)
        {
            currentState = State.PATROL;
            currentPatrolTarget = 0;
        }

    }

    private void updateChase()
    {
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
    }

    private void updateDie()
    {
        Color newColor = mat.color;
        if (newColor.a > 0)
        {
            explosion.Play();
            newColor.a -= Time.deltaTime;
            mat.color = newColor;
            gameObject.GetComponent<MeshRenderer>().material = mat;
        }
        else
        {
            Destroy(gameObject);
        }
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
        Ray r = new Ray(transform.position, transform.forward);
        float playerDist = (player.transform.position - transform.position).magnitude;
        if (Physics.Raycast(r, out RaycastHit hitInfo, chaseRange, obstacleMask))
        {
            return true;
        }

        return false;
    }


    void updateAttacking()
    {
        agent.isStopped = true;
        if ((transform.position - player.transform.position).magnitude < attackRange) {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            flash.Play();
            invulnerabilityStarted = invulnerabilityStarted + 0.01f;
            if ( invulnerabilityStarted > invulnerabilityDuration)
            {
                playerStats.takeDamage(damage);
                audioSource.PlayOneShot(sound_shoot);
                invulnerabilityStarted = 0;
            }
            
            flash.Play();
        }

    }

    void changeFromAttacking()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
            agent.isStopped = false;
            currentState = State.CHASE;
        }
    }
}
