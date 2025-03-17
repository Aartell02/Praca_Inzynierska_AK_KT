using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator animator;
    private bool isFacingRight = true;

    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        animator = GetComponent<Animator>();
        rb.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
        FlipSprite();
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        animator.SetFloat("Velocity",Math.Abs(rb.linearVelocity.magnitude));
    }

    void FlipSprite()
    {
        if (horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }
}
