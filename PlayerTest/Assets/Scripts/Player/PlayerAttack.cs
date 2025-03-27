using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float hitboxLifetime = 0.2f;


    public void Attack(Rigidbody2D rb)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0; //

        Vector3 direction = (mouseWorldPos - attackPoint.position).normalized;

        Vector2 spawnPos = (Vector2)attackPoint.position + (Vector2)direction * attackRange;

        GameObject hitbox = Instantiate(hitboxPrefab, spawnPos, Quaternion.identity);
        hitbox.GetComponent<Collider2D>().isTrigger = true;

        Destroy(hitbox, hitboxLifetime);
    }

}
