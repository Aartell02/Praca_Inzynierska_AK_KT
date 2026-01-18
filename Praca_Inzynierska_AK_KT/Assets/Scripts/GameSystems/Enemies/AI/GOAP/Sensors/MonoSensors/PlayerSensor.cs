using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystems.AI
{
	[RequireComponent(typeof(Collider2D))]
	public class PlayerSensor : MonoBehaviour
	{
		public Collider2D Collider;
		public delegate void PlayerEnterEvent(Transform player);
		public delegate void PlayerExitEvent(Vector2 lastKnownPosition);

		public event PlayerEnterEvent OnPlayerEnter;
		public event PlayerExitEvent OnPlayerExit;

		private void Awake()
		{
			Collider = GetComponent<Collider2D>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out PlayerState player))
			{
				OnPlayerEnter?.Invoke(player.transform);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.TryGetComponent(out PlayerState player))
			{
				OnPlayerExit?.Invoke(other.transform.position);
			}
		}
	}
}
