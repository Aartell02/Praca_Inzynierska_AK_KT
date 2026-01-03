using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class SoldierBrainBehaviour : MonoBehaviour
	{
		[SerializeField]
		private AIEnemyOrder lastOrder;
		private EnemyBrainData brainData;
		private PlayerSensor playerSensor;

		private AgentBehaviour agent;
		private GoapActionProvider provider;
		private GoapBehaviour goap;


		private void Awake()
		{
			this.goap = FindAnyObjectByType<GoapBehaviour>();
			this.agent = this.GetComponent<AgentBehaviour>();
			this.provider = this.GetComponent<GoapActionProvider>();
			this.brainData = this.GetComponent<EnemyBrainData>();

			this.playerSensor = this.GetComponentInChildren<PlayerSensor>();

			if (this.provider.AgentTypeBehaviour == null)
				this.provider.AgentType = this.goap.GetAgentType(EnemyType.Soldier.ToString());
		}

		private void OnEnable()
		{
			playerSensor.OnPlayerEnter += OnPlayerEnter;
			playerSensor.OnPlayerExit += OnPlayerExit;
		}

		private void Start()
		{
			provider.RequestGoal<GetOrderGoal>(true);
		}

		private void OnPlayerEnter(Transform Player)
		{
			//provider.RequestGoal<KillPlayerGoal>(true);
		}

		private void OnPlayerExit(Vector2 LastKnownPosition)
		{
		}
		public void SetOrder(AIEnemyOrder order)
		{
			if (lastOrder == order) return;
			switch (order)
			{
				case AIEnemyOrder.Guard:
					brainData.GiveOrder(order);
					provider.RequestGoal<GuardAltarGoal>(true);
					lastOrder = order;
					break;
			}
		}
	}
}
