using UnityEngine;

namespace GameSystems
{
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class MeleeHitbox : MonoBehaviour
	{
		private int damage;
		private LayerMask targetLayers;

		// Lista trafionych, żeby nie zadać obrażeń 2 razy temu samemu wrogowi w jednym cięciu
		private System.Collections.Generic.List<Collider2D> hitTargets = new System.Collections.Generic.List<Collider2D>();

		public void Initialize(int damageAmount, LayerMask layers, float duration)
		{
			this.damage = damageAmount;
			this.targetLayers = layers;

			// Zniszcz hitbox automatycznie po ustalonym czasie (np. 0.2s)
			Destroy(gameObject, duration);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			// Sprawdź czy obiekt jest na warstwie wroga (korzystając z masek bitowych)
			if (((1 << other.gameObject.layer) & targetLayers) != 0)
			{
				// Jeśli już go trafiliśmy tym konkretnym cięciem, ignoruj
				if (hitTargets.Contains(other)) return;

				hitTargets.Add(other);
				Debug.Log($"Hitbox trafił: {other.name}");

				if (other.TryGetComponent(out EnemyData enemy))
				{
					enemy.TakeDamage(damage);
				}
			}
		}
	}
}
