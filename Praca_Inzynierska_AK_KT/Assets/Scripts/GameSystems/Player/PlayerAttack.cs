using Core.Services;
using UnityEngine;

namespace GameSystems
{
	public class PlayerAttack : MonoBehaviour
	{
		[Header("Settings")]
		[SerializeField] private GameObject hitboxPrefab;
		[SerializeField] private float attackOffset = 1.0f;
		[SerializeField] private float attackDuration = 0.2f;
		[SerializeField] private int damageAmount = 20;

		[Header("Layers")]
		[SerializeField] private LayerMask enemyLayers;

		private Camera mainCam;
		private Animator animator;

		private float nextAttackTime = 0f;
		private Vector2 currentAttackPos;
		private Quaternion currentAttackRot;

		private void Awake()
		{
			mainCam = Camera.main;
			animator = GetComponent<Animator>();
		}

		private void Update()
		{
			if (Time.timeScale == 0) return;

			CalculateAttackTransform();

			if (PlayerInputService.LeftMouseButton > 0.5f && Time.time >= nextAttackTime)
			{
				Attack();
				nextAttackTime = Time.time + 1f/PlayerStats.Instance.AttackSpeed;
			}
		}

		private void CalculateAttackTransform()
		{
			Vector3 mouseScreenPos = PlayerInputService.MousePosition;

			if (mainCam != null)
			{
				mouseScreenPos.z = Mathf.Abs(mainCam.transform.position.z);
				Vector3 worldMousePos = mainCam.ScreenToWorldPoint(mouseScreenPos);
				worldMousePos.z = 0;

				Vector2 direction = (worldMousePos - transform.position).normalized;
				currentAttackPos = (Vector2)transform.position + (direction * attackOffset) + new Vector2(0f,0.5f);

				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				currentAttackRot = Quaternion.AngleAxis(angle, Vector3.forward);
			}
		}

		private void Attack()
		{
			if (animator != null) animator.SetTrigger("Attack");

			if (hitboxPrefab != null)
			{
				GameObject hitbox = Instantiate(hitboxPrefab, currentAttackPos, currentAttackRot);

				if (hitbox.TryGetComponent(out MeleeHitbox meleeScript))
				{
					meleeScript.Initialize(this.gameObject, damageAmount, enemyLayers, attackDuration);
				}
			}
			else
			{
				Debug.LogWarning("Brak przypisanego Hitbox Prefab!");
			}
		}

		private void OnDrawGizmos()
		{
			// Gizmos działają w edytorze nawet jak gra nie chodzi, 
			// ale wtedy 'mainCam' może być nullem lub pozycja myszy nieaktualna.
			// Rysujemy tylko gdy gra jest uruchomiona (Play Mode) dla precyzji.
			if (Application.isPlaying)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(currentAttackPos, 0.2f); // Mała kropka gdzie powstanie hitbox
				Gizmos.DrawLine(transform.position, currentAttackPos);
			}
		}
	}
}
