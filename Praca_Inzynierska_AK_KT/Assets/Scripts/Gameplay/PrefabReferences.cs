using Core;
using Core.Inspector;
using UnityEngine;

namespace Gameplay
{
	public class PrefabReferences : MonoBehaviour
	{
		[SerializeField]
		public GameObject Player;

		[SerializeField]
		[EnumArray(typeof(EnemyType))]
		public GameObject[] Enemy;

		[SerializeField]
		public GameObject Altar;

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
