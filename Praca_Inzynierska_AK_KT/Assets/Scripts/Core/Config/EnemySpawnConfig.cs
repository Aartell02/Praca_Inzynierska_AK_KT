using Core;
using Core.Inspector;
using System;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig", menuName = "Config/Gameplay/EnemySpawnConfig")]
	public class EnemySpawnConfig : ScriptableObject
	{
		[Header("Spawn configuration")]
		[SerializeField]
		[EnumArray(typeof(EnemyType))]
		public EnemySpawnData[] EnemySpawnData;
    }

	[Serializable]
	public struct EnemySpawnData
	{
		public EnemyType Type;
		public int Count;
	}
}
