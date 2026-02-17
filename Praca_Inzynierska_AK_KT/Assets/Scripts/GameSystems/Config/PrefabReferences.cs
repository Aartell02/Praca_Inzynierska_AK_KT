using Core;
#if UNITY_EDITOR
using Editor;
#endif
using UnityEngine;

namespace GameSystems
{
	public class PrefabReferences : MonoBehaviour
	{
		[Header("Player")]
		[SerializeField] public GameObject Player;

		[Header("Enemies")]
#if UNITY_EDITOR
		[EnumArray(typeof(EnemyType))]
#endif
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
