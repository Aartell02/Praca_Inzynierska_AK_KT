using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyRoaming : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float roamingRange = 1f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private Vector2 moveDir;
    private bool isFacingRight = true;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
        animator.SetFloat("move", moveDir.magnitude);
        FlipSprite();
    }
    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = (targetPosition - rb.position).normalized;
    }
    public Vector2 GetRoamingPosition()
    {
        return rb.position + new Vector2(Random.Range(-roamingRange, roamingRange), Random.Range(-roamingRange, roamingRange));
    }
    void FlipSprite()
    {
        if (moveDir.x < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            sprite.flipX = !sprite.flipX;
        }
    }

}
