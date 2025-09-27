using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
	public class GameplayViewModel : MonoBehaviour
	{
		[SerializeField]
		public EnemySpawnConfig enemyConfig;

		public static GameplayViewModel Instance { get; private set; }

		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		public EnemySpawnConfig GetConfig() => enemyConfig;
	}
}
