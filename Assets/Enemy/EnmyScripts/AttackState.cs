using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    private NavMeshAgent agent;
    private Transform player;
    private Enemy enemy;

    private float attackRange = 2f;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;
    public AttackState(GameObject owner, NavMeshAgent agent, Transform player, Enemy enemy) : base(owner)
    {
        this.agent = agent;
        this.player = player;
        this.enemy = enemy;
    }

    public override void Enter()
    {
        agent.isStopped = true;
        
        lastAttackTime = Time.time - attackCooldown;
        Debug.Log("Entering Attack State");
        
    }
    public override void Update()
    {
        if (player == null) return;
        
        float distance = Vector3.Distance(owner.transform.position, player.position);
        
        Vector3 direction = (player.position - owner.transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            owner.transform.rotation = Quaternion.LookRotation(direction);
        }

        if (distance > attackRange + 1f)
        {
            enemy.ChangeState(new ChaseState(owner, agent, player,enemy));
            return;
        }

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            PerformAttack();
            enemy.animator.SetTrigger("AttackTrigger");
        }
    }

    private void PerformAttack()
    {
        Debug.Log("Enemy Atacks!");
    }
    
    public override void Exit()
    {
        agent.isStopped = false;
    }
}
