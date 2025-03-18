using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerControls;
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
        mySprite = rb.GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    void Start()
    {
    }

    void Update()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        rb.MovePosition(rb.position + movement * (speed * Time.fixedDeltaTime));
        animator.SetFloat("move",movement.magnitude);

        FlipSprite();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isRolling", true);
            speed = rollSpeed;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Roll") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f)
        {
            animator.SetBool("isRolling", false);
            speed = runningSpeed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isAttacking", true);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f)
        {
            animator.SetBool("isAttacking", false);

        }
            
    }
    void FixedUpdate()
    {
 
    }

    void FlipSprite()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < playerScreenPoint.x )
        {
            mySprite.flipX = true;
        }
        else
        {
            mySprite.flipX = false;
        }
    }
}
