using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public PlayerInputActions playerControls;
    private PlayerAttack playerAttack;
    private Animator animator;
    private Vector2 movement;
    private float speed = 4.0f;
    private float runningSpeed = 4.0f;
    private float rollSpeed = 8.0f;
    private Rigidbody2D rb;
    private SpriteRenderer mySprite;
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
        playerAttack = GetComponent<PlayerAttack>();
}
    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Movement.Roll.started += OnRoll;
        playerControls.Combat.Attack.started += OnAttack;


    }
    private void OnDisable()
    {
        playerControls.Movement.Roll.started -= OnRoll;
        playerControls.Combat.Attack.started -= OnAttack;
        playerControls.Disable();
    }

    void FixedUpdate()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        rb.MovePosition(rb.position + movement * (speed * Time.fixedDeltaTime));
        animator.SetFloat("move",movement.magnitude);
        FlipSprite();

    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        animator.SetBool("isRolling", true);
        if(animator.GetBool("isRolling")) speed = rollSpeed;
    }
    public void OnRollFinishEvent()
    {
        animator.SetBool("isRolling", false);
        speed = runningSpeed;
    }
        
    void OnAttack(InputAction.CallbackContext context) 
    {

        animator.SetBool("isAttacking", true);
        playerAttack.Attack(rb);
    }
    void OnAttackFinishEvent()
    {
        animator.SetBool("isAttacking", false);

    }
    void FlipSprite()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        mySprite.flipX = mousePos.x < playerScreenPoint.x;
    }
}
