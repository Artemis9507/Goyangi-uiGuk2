public class StateMachine
{
   public State currentState;
   
   private bool isStopped = false;

   public void ChangeState(State newState)
   {
      currentState?.Exit();
      
      currentState = newState;
      
      currentState?.Enter();
      
   }

   public void Update()
   {
      if (isStopped) return;
      currentState?.Update();
   }
   
   public void Stop()
   {
      isStopped = true;
   }
}
