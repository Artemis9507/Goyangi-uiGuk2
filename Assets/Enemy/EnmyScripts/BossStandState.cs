using UnityEngine;

public class BossStandState : State
{
    private Boss boss;
    private Animator animator;
    private float postDelay;
    private bool started = false;

    public BossStandState(GameObject owner, Boss boss, float postStandDelay = 0.1f) : base(owner)
    {
        this.boss = boss;
        this.animator = boss.Animator;
        this.postDelay = postStandDelay;
    }

    public override void Enter()
    {
        if (boss.Agent != null)
        {
            boss.Agent.isStopped = true;
            boss.Agent.ResetPath();
        }
        
        if (animator != null)
        {
            animator.SetTrigger(boss.standTrigger);
        }

        started = true;
    }

    public override void Update()
    {
        if (!started) return;

        if (boss.Health != null && boss.Health.IsDead)
        {
            return;
        }

        if (animator == null)
        {
            ToChase();
            return;
        }
        
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        
        bool inStandState = info.IsName(boss.standTrigger) || info.IsName("StandUp") || info.IsName("Stand"); 
        
        if (!inStandState)
        {
            return;
        }

        if (info.normalizedTime >= 1f)
        {
            if (postDelay > 0f)
            {
                postDelay -= Time.deltaTime;
                return;
            }

            ToChase();
        }
    }
    
    private void ToChase()
    {
        if (boss.Health != null && boss.Health.IsDead) return;
        
        if (boss.Agent != null)
        {
            boss.Agent.isStopped = false;
        }
        
        boss.ChangeState(new ChaseState(owner, boss.Agent, boss.Player, boss));
    }

    public override void Exit()
    {
        
    }
}
