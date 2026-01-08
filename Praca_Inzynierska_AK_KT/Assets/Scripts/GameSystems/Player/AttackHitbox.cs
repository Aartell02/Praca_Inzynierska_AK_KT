using UnityEngine;

namespace GameSystems
{
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class MeleeHitbox : MonoBehaviour
	{
		private int damage;
		private LayerMask targetLayers;

		private System.Collections.Generic.List<Collider2D> hitTargets = new System.Collections.Generic.List<Collider2D>();

		public void Initialize(int damageAmount, LayerMask layers, float duration)
		{
			this.damage = damageAmount;
			this.targetLayers = layers;

			Destroy(gameObject, duration);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (((1 << other.gameObject.layer) & targetLayers) != 0)
			{
				if (hitTargets.Contains(other)) return;

				hitTargets.Add(other);
				Debug.Log($"Hitbox trafił: {other.name}");

				if (other.TryGetComponent(out LifeStateData enemy))
				{
					enemy.TakeDamage(damage);
				}
			}
		}
	}
}
