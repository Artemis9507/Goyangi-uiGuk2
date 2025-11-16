using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int horizontal;
    int vertical;
    int isJumping;
    int isCrouching;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
        isJumping = Animator.StringToHash("IsJumping");
        isCrouching = Animator.StringToHash("IsCrouching");
    }
    
    public void UpdateAnimatorValues(float horizontalM, float verticalM)
    {
        
        animator.SetFloat(horizontal, horizontalM, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, verticalM, 0.1f, Time.deltaTime);
    }
    public void PlayTargetAnimation(string animationName, bool isInteracting)
    {
        animator.CrossFade(animationName, 0.1f);
    }
    public void SetCrouching(bool state)
    {
        animator.SetBool("IsCrouching", state);
    }

    public void SetJumping()
    {
        animator.SetTrigger("IsJumping");
    }

    public void SetAttacking()
    {
        animator.SetTrigger("IsAttacking");
    }
    
}
