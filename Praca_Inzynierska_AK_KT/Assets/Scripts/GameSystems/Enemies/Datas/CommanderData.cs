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

		private EnemyBrainData brainData;
		internal void AddSoldier() => SoldiersCount++;

		private void Start()
		{
			EnemySharedData.Commanders.Add(transform);
			TroopsToCommand = new List<GameObject>[Enum.GetValues(typeof(EnemyType)).Length];
			for(int i = 0; i < TroopsToCommand.Length; i++)
				TroopsToCommand[i] = new();
		}

		private void Update()
		{
			
		}
	}
}
