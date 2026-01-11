using Core;
using Core.Inspector;
using UnityEngine;

namespace GameSystems
{
	public class PrefabReferences : MonoBehaviour
	{
		[Header("Player")]
		[SerializeField] public GameObject Player;

		[Header("Enemies")]
		[EnumArray(typeof(EnemyType))]
		[SerializeField] public GameObject[] Enemy;

		[Header("Structures")]
		[SerializeField] public GameObject Altar;
		[SerializeField] public GameObject ExitTrigger;


		public static PrefabReferences Instance { get; private set; }

		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}
			Instance = this;
		}
	}
}
