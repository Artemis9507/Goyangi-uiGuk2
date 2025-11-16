using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    PlayerControler playerControler;
    AnimatorManager animatorManager;
    PauseMenu pauseMenu;
    private PlayerAttack playerAttack;
    
    public Vector2 moveInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;
        
    public float moveAmount;
    public float vertical;
    public float horizontal;
    
    public bool sprintInput;
    public bool jumpInput;
    public bool crouchInput;
    public bool attackInput;
    
    public bool pauseInput;
    

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerAttack = GetComponent<PlayerAttack>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        
    }
    private void OnEnable()
    {
        if (playerControler == null)
        {
            playerControler = new PlayerControler();
            
            playerControler.Player.Move.performed += i => moveInput = i.ReadValue<Vector2>();
            
            playerControler.Player.Look.performed += i => cameraInput = i.ReadValue<Vector2>();
            
            playerControler.Player.Sprint.performed += i => sprintInput = true;
            playerControler.Player.Sprint.canceled += i => sprintInput = false;
            
            playerControler.Player.Jump.performed += i => jumpInput = true;
            playerControler.Player.Jump.canceled += i => jumpInput = false;
            
            playerControler.Player.Attack.performed += i => attackInput = true;
            playerControler.Player.Attack.canceled  += i => attackInput = false;
            
            playerControler.Player.Crouch.performed += i => crouchInput = !crouchInput;
            
            playerControler.Player.Pause.performed += i => pauseInput = true;
            playerControler.Player.Pause.canceled += i => pauseInput = false;
        }
        
        playerControler.Enable();
    }

    private void OnDisable()
    {
        playerControler?.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleAttackInput();
        HandlePauseInput();
    }

    private void HandleMovementInput()
    {
        vertical = moveInput.y;
        horizontal = moveInput.x;
        
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
        
        moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }
    
    public void HandleAttackInput()
    {
        if (attackInput)
        {
            animatorManager.SetAttacking();
            
            StartCoroutine(Delay(0.25f));
            attackInput = false; 
        }
    }
    
    private void HandlePauseInput()
    {
        if (pauseInput)
        {
            if (pauseMenu == null) pauseMenu = FindObjectOfType<PauseMenu>();
            pauseMenu?.TogglePause();
            pauseInput = false;
        }
    }
    
    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerAttack.PreformeAttack();
    }
}

