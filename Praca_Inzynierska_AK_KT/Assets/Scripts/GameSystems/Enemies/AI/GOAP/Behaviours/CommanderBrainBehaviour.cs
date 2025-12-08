using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

namespace GameSystems.AI
{
	public class CommanderBrainBehaviour : MonoBehaviour
	{
		private PlayerSensor playerSensor;

		private AgentBehaviour agent;
		private GoapActionProvider provider;
		private GoapBehaviour goap;

		private IGoal previousGoal;

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
		}

		private void OnPlayerEnter(Transform Player)
		{
			Debug.Log("KillPlayer Requested");
			if(provider.CurrentPlan != null) 
				previousGoal = provider.CurrentPlan.Goal;
			provider.RequestGoal<KillPlayerGoal>(true);

		}

		private void OnPlayerExit(Vector2 LastKnownPosition)
		{
			if (provider.CurrentPlan != null)
				previousGoal = provider.CurrentPlan.Goal;
			provider.RequestGoal<WanderGoal>(true);
		}
	}
}
