using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using System;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

namespace GameSystems.AI
{
	public class ScoutBrainBehaviour : MonoBehaviour
	{
		private AgentBehaviour agent;
		private GoapActionProvider provider;
		private GoapBehaviour goap;

		private PlayerSensor playerSensor;

		private EnemyBrainData brainData;

		private void Awake()
		{
			this.goap = FindAnyObjectByType<GoapBehaviour>();
			this.agent = this.GetComponent<AgentBehaviour>();
			this.provider = this.GetComponent<GoapActionProvider>();
			if (this.provider.AgentTypeBehaviour == null)
				this.provider.AgentType = this.goap.GetAgentType(EnemyType.Scout.ToString());

			this.playerSensor = this.GetComponentInChildren<PlayerSensor>();

			this.brainData = this.GetComponent<EnemyBrainData>();
		}

		private void OnEnable()
		{
			playerSensor.OnPlayerEnter += OnPlayerEnter;
			playerSensor.OnPlayerExit += OnPlayerExit;
		}
		private void Start()
		{
			brainData.SetGoal(AIEnemyGoal.None);
			provider.RequestGoal<GetOrderGoal>(true);
		}
		private void Update()
		{
			if(this.brainData.dirty)
				SetGoal(brainData.Goal);
			if (!provider.CurrentPlan.IsNull())
				Debug.Log($"{gameObject.name} Goal: {provider.CurrentPlan.Goal} Action {provider.CurrentPlan.Action}");
		}

		private void OnPlayerEnter(Transform Player) => brainData.SetGoal(AIEnemyGoal.Attack);

		private void OnPlayerExit(Vector2 LastKnownPosition) => brainData.SetGoal(brainData.Order);

		public void GiveOrder(AIEnemyGoal goal) => brainData.SetGoal(goal, true);

		void SetGoal(AIEnemyGoal goal)
		{
			switch (goal)
			{
				case AIEnemyGoal.None:
					provider.RequestGoal<GetOrderGoal>(true);
					break;
				case AIEnemyGoal.Scout:
					provider.RequestGoal<DeliverPillarLocationsGoal>(true);
					break;

				case AIEnemyGoal.Guard:
					//provider.RequestGoal<GuardAltarGoal>(true);
					break;
			}

			brainData.dirty = false;
		}
	}
}
