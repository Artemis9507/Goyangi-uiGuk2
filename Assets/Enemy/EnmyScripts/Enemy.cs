using System;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{ 
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    
    public Transform[] patrolPoints;
    
    [Header("Vision Settings")]
    public float visionDistance = 10f;
    public float visionAngle = 120f;
    public LayerMask obstacleMask;
    public Transform player; 
    
    [Header("Animation Settings")]
    public Animator animator;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    
    
    
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = new StateMachine();
    }

    public void Start()
    {
        stateMachine.ChangeState(new PatrolState(gameObject, agent,  patrolPoints, this));
    }

    public void Update()
    {
        stateMachine.Update();

        if (player != null)
        {
            
            if (stateMachine.currentState is PatrolState)
            {
                if (CanSeePlayer())
                {
                    ChangeState(new ChaseState(gameObject, agent, player, this));
                }
                
            }
        }
    }

    public void ChangeState(State newState)
    {
        stateMachine.ChangeState(newState);
    }

    public bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Vector3 origin = transform.position + Vector3.up * 1.5f; 

        if (Vector3.Angle(transform.forward, directionToPlayer) < visionAngle / 2)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= visionDistance)
            {
                if (!Physics.Raycast(origin, directionToPlayer, visionDistance, obstacleMask))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
