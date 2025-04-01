using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyStats>())
        {
            EnemyStats enemyHealth = other.gameObject.GetComponent<EnemyStats>();
            enemyHealth.TakeDamage(damageAmount);
        }
    }
}
