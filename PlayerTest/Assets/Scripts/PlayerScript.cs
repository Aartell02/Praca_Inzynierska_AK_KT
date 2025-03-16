using UnityEngine;

public class PlayerScript1 : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 4.0f;
    private Rigidbody2D rb;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        rb.linearVelocity = new Vector2 (horizontal*speed, vertical*speed);
    }
}
