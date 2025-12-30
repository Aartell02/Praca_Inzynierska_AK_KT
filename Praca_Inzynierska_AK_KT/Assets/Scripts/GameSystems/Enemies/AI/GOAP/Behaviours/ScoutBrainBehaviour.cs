using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

namespace GameSystems.AI
{
	public class ScoutBrainBehaviour : MonoBehaviour
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
				this.provider.AgentType = this.goap.GetAgentType(EnemyType.Scout.ToString());
		}

		private void OnEnable()
		{
			playerSensor.OnPlayerEnter += OnPlayerEnter;
			playerSensor.OnPlayerExit += OnPlayerExit;
		}
		private void Start()
		{
			provider.RequestGoal<GetOrderGoal>(true);
			lastOrder = AIEnemyOrder.None;
		}
		private void Update()
		{
			if(!provider.CurrentPlan.IsNull())
				Debug.Log($"{gameObject.name} Goal: {provider.CurrentPlan.Goal} Action {provider.CurrentPlan.Action}");
			if (brainData.Order is AIEnemyOrder.None && lastOrder is not AIEnemyOrder.None)
			{
				provider.RequestGoal<GetOrderGoal>(false);
				lastOrder = AIEnemyOrder.None;
			}
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
			switch (order)
			{
				case AIEnemyOrder.Scout:
					brainData.GiveOrder(order);
					provider.RequestGoal<DeliverPillarLocationsGoal>(true);
					lastOrder = order;
					break;
			}
		}
	}
}
