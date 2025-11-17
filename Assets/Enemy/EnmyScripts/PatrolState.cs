using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private NavMeshAgent agent;
    private Transform[] patrolPoints;
    private int currentPointIndex = 0;
    private Enemy enemy;
    
    
    public PatrolState(GameObject owner, NavMeshAgent agent, Transform[] patrolPoints, Enemy enemy) : base(owner)
    {
        this.agent = agent;
        this.patrolPoints = patrolPoints;
        this.enemy = enemy;
    }

    public override void Enter()
    {
        if (patrolPoints.Length == 0)
        {
            Debug.Log("No Patrol Points");
        } 
        
        agent.isStopped = false;
        agent.speed = enemy.patrolSpeed;
        enemy.animator.SetFloat(enemy.SpeedTrigger, agent.speed);
        agent.SetDestination(patrolPoints[currentPointIndex].position);
    }

    public override void Update()
    {
        if (enemy.health.IsDead) return;
        
        if (patrolPoints.Length == 0) return;
        
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
        
        enemy.animator.SetFloat(enemy.SpeedTrigger, agent.speed);
    }
    public override void Exit()
    {
        agent.isStopped = true;
    }
}
