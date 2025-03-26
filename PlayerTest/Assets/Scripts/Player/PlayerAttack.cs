using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject hitboxPrefab; 
    public Transform attackPoint; 
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public float hitboxLifetime = 0.2f;


    public void Attack(Rigidbody2D rb)
    {
        // Get mouse position in world space
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0; // Ensure it's in 2D space

        // Calculate correct direction from player to mouse
        Vector3 direction = (mouseWorldPos - (Vector3)rb.position).normalized;

        // Determine the spawn position of the hitbox around the player
        Vector2 spawnPos = rb.position;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            spawnPos += new Vector2(Mathf.Sign(direction.x) * attackRange, 0);
        }
        else
        {
            spawnPos += new Vector2(0, Mathf.Sign(direction.y) * attackRange);
        }

        // Instantiate the hitbox at the correct position
        GameObject hitbox = Instantiate(hitboxPrefab, spawnPos, Quaternion.identity);
        hitbox.GetComponent<Collider2D>().isTrigger = true;

        // Destroy hitbox after some time
        Destroy(hitbox, 0.5f);
    }

}
