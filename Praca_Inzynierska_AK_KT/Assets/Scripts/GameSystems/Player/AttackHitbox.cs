using UnityEngine;
using System.Collections.Generic;

namespace GameSystems
{
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class MeleeHitbox : MonoBehaviour
	{
		private GameObject attacker;
		private int damage;
		private float knockbackForce = 500f;
		private LayerMask targetLayers;

		private List<Collider2D> hitTargets = new List<Collider2D>();

		public void Initialize(GameObject attacker, int damageAmount, LayerMask layers, float duration)
		{
			this.attacker = attacker;
			this.damage = damageAmount;
			this.targetLayers = layers;

			Destroy(gameObject, duration);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (((1 << other.gameObject.layer) & targetLayers) == 0) return;

			if (hitTargets.Contains(other)) return;
			hitTargets.Add(other);

			if (other.TryGetComponent(out LifeStateData enemy))
			{
				enemy.TakeDamage(damage, attacker);
			}

			ApplyKnockback(other);
		}

		private void ApplyKnockback(Collider2D target)
		{
			if (target.TryGetComponent(out Rigidbody2D rb))
			{
				Vector2 direction = (target.transform.position - attacker.transform.position).normalized;

				rb.linearVelocity = Vector2.zero;

				rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
			}
		}
	}
}
