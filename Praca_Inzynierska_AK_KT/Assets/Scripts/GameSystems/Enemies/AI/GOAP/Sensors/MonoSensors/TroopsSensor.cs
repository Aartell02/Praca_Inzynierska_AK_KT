using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystems.AI
{
	[RequireComponent(typeof(Collider2D))]
	public class TroopsSensor : MonoBehaviour
	{
		[SerializeField]
		private Collider2D Collider;
		public delegate void UnitEnterEvent(GameObject unit);
		public delegate void UnitExitEvent(GameObject unit);

		public event UnitEnterEvent OnUnitEnter;
		public event UnitExitEvent OnUnitExit;

		private void Awake()
		{
			Collider = GetComponent<Collider2D>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out EnemyData unit))
			{
				OnUnitEnter?.Invoke(unit.gameObject);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.TryGetComponent(out EnemyData unit))
			{
				OnUnitExit?.Invoke(unit.gameObject);
			}
		}


	}
}
