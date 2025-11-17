using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{ 
    public StateMachine stateMachine;
    
    protected NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    public EnemyHealth health;
    public EnemyHealth Health => health;

    public Animator animator;
    public Animator Animator => animator;

    public EnemyAttack enemyAttack;

    public Transform player;
    public Transform Player => player;

    public Transform[] patrolPoints;
    
    public int SpeedTrigger;
    public int DieTrigger;
    public int AttackTrigger;

    [Header("Vision Settings")]
    public float visionDistance = 10f;
    public float visionAngle = 120f;
    public LayerMask obstacleMask;

    [Header("Animation Settings")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    [Header("Stats")]
    public float damage = 10f;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        health = GetComponent<EnemyHealth>();
        
        SpeedTrigger = Animator.StringToHash("Speed");
        AttackTrigger = Animator.StringToHash("AttackTrigger");
        DieTrigger = Animator.StringToHash("Die");
        
        

        stateMachine = new StateMachine();

        OnAwakeCompleted();
    }
    
    protected virtual void OnAwakeCompleted() { }

    public void Start()
    {
        stateMachine.ChangeState(new PatrolState(gameObject, agent,  patrolPoints, this));
    }

    public void Update()
    {
        if (health.IsDead) return;
        
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
    
    public void StopAI()
    {
            stateMachine.Stop();
        
    }
}
