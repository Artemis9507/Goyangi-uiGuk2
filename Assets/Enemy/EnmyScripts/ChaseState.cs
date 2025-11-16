using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    private NavMeshAgent agent;
    private Transform player;
    private Enemy enemy;
    private float attackRange = 2f;
    
    public ChaseState(GameObject owner, NavMeshAgent agent, Transform player, Enemy enemy) : base(owner)
    {
        this.agent = agent;
        this.player = player;
        this.enemy = enemy;
    }

    public override void Enter()
    {
        agent.isStopped = false;
        agent.speed = enemy.chaseSpeed;
        enemy.animator.SetFloat("Speed", agent.speed);
    }
    public override void Update()
    {
        if (enemy.health.isDead) return;
        
        if (player == null) return;
        
        if (!enemy.CanSeePlayer())
        {
            enemy.ChangeState(new PatrolState(owner, agent, enemy.patrolPoints, enemy));
        }
        
        agent.SetDestination(player.position);
        
        float distance = Vector3.Distance(owner.transform.position, player.position);
        

        if (distance <= attackRange)
        {
            enemy.ChangeState(new AttackState(owner, agent, player, enemy));
        }
        
        enemy.animator.SetFloat("Speed", agent.speed);
    }

    public override void Exit()
    {
        agent.isStopped = true;
    }
    
}
