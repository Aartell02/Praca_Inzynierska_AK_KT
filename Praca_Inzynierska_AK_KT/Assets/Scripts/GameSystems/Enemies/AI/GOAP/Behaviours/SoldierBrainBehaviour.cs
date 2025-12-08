using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class SoldierBrainBehaviour : MonoBehaviour
	{
		private EnemyData enemyStats;
		private PlayerSensor playerSensor;

		private AgentBehaviour agent;
		private GoapActionProvider provider;
		private GoapBehaviour goap;


		private void Awake()
		{
			this.goap = FindAnyObjectByType<GoapBehaviour>();
			this.agent = this.GetComponent<AgentBehaviour>();
			this.provider = this.GetComponent<GoapActionProvider>();

			this.playerSensor = this.GetComponent<PlayerSensor>();

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
			Debug.Log("Wander Requested");
			provider.RequestGoal<WanderGoal>();
		}

		private void OnPlayerEnter(Transform Player)
		{
			Debug.Log("KillPlayer Requested");
			provider.RequestGoal<KillPlayerGoal>(true);
		}

		private void OnPlayerExit(Vector2 LastKnownPosition)
		{
			provider.RequestGoal<WanderGoal>(true);
		}
	}
}
