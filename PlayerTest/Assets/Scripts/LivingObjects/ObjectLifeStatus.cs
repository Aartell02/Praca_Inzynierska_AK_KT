using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectLifeStatus : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deadBodyPrefab;
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
            Destroy(gameObject);
            animator.ResetTrigger("TookHit");
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = transform.rotation;
            Instantiate(deadBodyPrefab, spawnPosition, spawnRotation);
            Debug.Log("Prefab zainstancjonowany: " + deadBodyPrefab.name);
            ObjectDeathStatus db = deadBodyPrefab.GetComponent<ObjectDeathStatus>();
            if (db == null)
                Debug.LogError("DeadBody script NIE ZNALEZIONY!");
            else
                Debug.Log("DeadBody script znaleziony!");
            
        }
    }
}
