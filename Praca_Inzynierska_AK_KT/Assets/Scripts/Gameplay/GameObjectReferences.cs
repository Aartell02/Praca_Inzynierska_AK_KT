using Core;
using Core.Inspector;
using UnityEngine;

namespace Gameplay
{
	public class GameObjectReferences : MonoBehaviour
	{
		[SerializeField]
		public GameObject Player;

		[SerializeField]
		[EnumArray(typeof(EnemyType))]
		public GameObject[] Enemy;

		public static GameObjectReferences Instance { get; private set; }

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
