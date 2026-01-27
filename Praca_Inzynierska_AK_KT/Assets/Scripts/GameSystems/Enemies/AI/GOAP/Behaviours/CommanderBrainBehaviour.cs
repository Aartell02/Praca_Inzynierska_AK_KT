using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

namespace GameSystems.AI
{
	public class CommanderBrainBehaviour : MonoBehaviour
	{
		[SerializeField]
		internal List<GameObject>[] TroopsToCommand;

		private PlayerSensor playerSensor;
		private TroopsSensor troopsSensor;

		private AgentBehaviour agent;
		private GoapActionProvider provider;
		private GoapBehaviour goap;
		private EnemyBrainData brainData;

		private IGoal previousGoal;

		private void Awake()
		{
			this.goap = FindAnyObjectByType<GoapBehaviour>();
			this.agent = this.GetComponent<AgentBehaviour>();
			this.provider = this.GetComponent<GoapActionProvider>();
			if (this.provider.AgentTypeBehaviour == null)
				this.provider.AgentType = this.goap.GetAgentType(EnemyType.Commander.ToString());

			this.playerSensor = this.GetComponentInChildren<PlayerSensor>();
			this.troopsSensor = this.GetComponentInChildren<TroopsSensor>();

			this.brainData = this.GetComponent<EnemyBrainData>();

			EnemySharedData.Commanders.Add(gameObject);

			TroopsToCommand = new List<GameObject>[Enum.GetValues(typeof(EnemyType)).Length];
			for (int i = 0; i < TroopsToCommand.Length; i++)
				TroopsToCommand[i] = new();
		}

		private void OnEnable()
		{
			playerSensor.OnPlayerEnter += OnPlayerEnter;
			playerSensor.OnPlayerExit += OnPlayerExit;
			troopsSensor.OnUnitEnter += OnUnitEnter;
			troopsSensor.OnUnitExit += OnUnitExit;
		}

		private void Start()
		{
			SetGoal(AIEnemyGoal.None);
		}

		private void Update()
		{
			if (this.brainData.dirty)
				SetGoal(brainData.Goal);
		}

		private void OnPlayerEnter(Transform Player) => brainData.SetGoal(AIEnemyGoal.Attack);

		private void OnPlayerExit(Vector2 LastKnownPosition) => brainData.SetGoal(brainData.Order);

		void SetGoal(AIEnemyGoal goal)
		{
			switch (goal)
			{
				case AIEnemyGoal.Attack:
					provider.RequestGoal<KillPlayerGoal>(true);
					break;
				case AIEnemyGoal.None:
					provider.RequestGoal<StrategizeGoal>(true);
					break;
			}
			brainData.dirty = false;
		}

		private void OnUnitEnter(GameObject unit)
		{
			var unitData = unit.GetComponent<EnemyData>();
			TroopsToCommand[(int)unitData.EnemyType].Add(unit);
		}

		private void OnUnitExit(GameObject unit)
		{
			var unitData = unit.GetComponent<EnemyData>();
			TroopsToCommand[(int)unitData.EnemyType].Remove(unit);
		}

	}
}
