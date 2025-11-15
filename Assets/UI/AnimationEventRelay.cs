using UnityEngine;
using UnityEngine.Events;


public class AnimationEventRelay : MonoBehaviour
{
    public UnityEvent onAnimationEvent;
    
    public void TriggerEvent()
    {
        onAnimationEvent?.Invoke();
    }
}
