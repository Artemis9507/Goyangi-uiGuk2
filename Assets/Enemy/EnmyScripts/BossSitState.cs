using UnityEngine;

public class BossSitState : State
{
   private Boss boss;

   public BossSitState(GameObject owner, Boss boss) : base(owner)
   {
      this.boss = boss;
   }

   public override void Enter()
   {
      if (boss.Agent != null)
      {
         boss.Agent.isStopped = true;
         boss.Agent.ResetPath();
      }

      if (boss.Animator != null)
      {
         boss.Animator.SetTrigger(boss.sitTrigger);
      }
   }

   public override void Update()
   {
      
   }

   public override void Exit()
   {
      
   }
}
