using Core;
using Core.Inspector;
using System;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemySpawnConfig", menuName = "Configs/EnemySpawnConfig")]
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
