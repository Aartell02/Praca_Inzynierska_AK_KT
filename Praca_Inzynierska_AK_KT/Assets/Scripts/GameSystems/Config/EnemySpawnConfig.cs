using Core;
#if UNITY_EDITOR
using Editor;
#endif
using System;
using UnityEngine;

namespace GameSystems.Config
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig", menuName = "Config/EnemySpawnConfig")]
	public class EnemySpawnConfig : ScriptableObject
	{
		[Header("Spawn configuration")]
		[SerializeField]
#if UNITY_EDITOR
		[EnumArray(typeof(EnemyType))]
#endif
		public EnemySpawnData[] EnemySpawnData;
    }

	[Serializable]
	public struct EnemySpawnData
	{
		public EnemyType Type;
		public int Count;
	}
}
