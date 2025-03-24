using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject hitboxPrefab; 
    public Transform attackPoint; 
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public float hitboxLifetime = 0.2f;


    public void Attack()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0; // Keep it in 2D space

        Vector3 characterPos = transform.position;
        Vector3 direction = (mouseWorldPos - characterPos).normalized;

        Vector3 spawnPos = characterPos;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            spawnPos += new Vector3(Mathf.Sign(direction.x) * attackRange, 0, 0);
        }
        else
        {
            spawnPos += new Vector3(0, Mathf.Sign(direction.y) * attackRange, 0);
        }

        GameObject hitbox = Instantiate(hitboxPrefab, spawnPos, Quaternion.identity);
        hitbox.GetComponent<Collider2D>().isTrigger = true; // Ensure it's a trigger
        Destroy(hitbox, 0.5f); // Auto-destroy after 0.5 seconds
    }

}
