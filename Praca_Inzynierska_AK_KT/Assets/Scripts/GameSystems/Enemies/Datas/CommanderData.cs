using Core;
using GameSystems.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystems
{
	public class CommanderData : MonoBehaviour
	{
		[SerializeField]
		internal List<GameObject>[] TroopsToCommand;

		internal int SoldiersCount { get; private set; }

		internal int ScoutsCount { get; private set; }

		internal void AddSoldier() => SoldiersCount++;

		private void Start()
		{
			var result = EnemySharedData.Commanders[0] = transform;
			TroopsToCommand = new List<GameObject>[Enum.GetValues(typeof(EnemyType)).Length];
			for(int i = 0; i < TroopsToCommand.Length; i++)
				TroopsToCommand[i] = new();
			Debug.Log(result);
		}

		private void Update()
		{
			
		}
	}
}
