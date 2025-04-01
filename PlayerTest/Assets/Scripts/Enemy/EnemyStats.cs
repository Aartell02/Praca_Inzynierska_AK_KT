using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    private Animator animator;
    private int currentHealth;
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        animator.SetTrigger("TookHit");
        DetectDeath();
    }
    private void DetectDeath()
    {
        if (currentHealth <= 0) {
            animator.ResetTrigger("TookHit");
            animator.SetBool("IsDead",true);
        }
    }

    void OnAttackFinishEvent()
    {
        animator.speed = 0;
    }
}
