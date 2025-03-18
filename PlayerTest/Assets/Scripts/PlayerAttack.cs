using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject swordHitbox; 
    public Transform attackPoint; 
    public float attackRange = 1f;
    public LayerMask enemyLayers;


    public void Attack()
    {
        Vector2 attackDirection = GetMouseDirection();
        SpawnHitbox(attackDirection);
    }

    Vector2 GetMouseDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        return direction;
    }

    void SpawnHitbox(Vector2 direction)
    {
        GameObject hitbox = Instantiate(swordHitbox, attackPoint.position, Quaternion.identity);
        hitbox.transform.right = direction; 

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
        }

        Destroy(hitbox, 0.2f); 
    }
}
