using UnityEngine;
public class Boss : Enemy
{
    [Header("Boss Settings")]
    public bool isActivated = false;

    public string sitTrigger = "Sit";
    public string standTrigger = "StandUp";

    public float postStandDelay = 0.1f;

    protected override void OnAwakeCompleted()
    {
        isActivated = false;
        
        if (Agent != null)
            Agent.isStopped = true;

        if (enemyAttack == null)
            enemyAttack = GetComponentInChildren<EnemyAttack>();
        
        if (Player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }
    new void Start()
    {
        ChangeState(new BossSitState(gameObject, this));
    }
    
    new void Update()
    {
        if (Health != null && Health.IsDead) return;
        
        if (!isActivated)
        {
            stateMachine.Update();
            return;
        }

        base.Update();
    }
    
    public void ActivateBoss()
    {
        if (isActivated || (Health != null && Health.IsDead)) return;

        isActivated = true;
        
        stateMachine.ChangeState(new BossStandState(gameObject, this, postStandDelay));
    }
}
