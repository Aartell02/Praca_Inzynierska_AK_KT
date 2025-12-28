using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

namespace GameSystems.AI
{
	public class CommanderBrainBehaviour : MonoBehaviour, IInjectable
	{
		private EnemyConfig enemyConfig;
		private PlayerSensor playerSensor;
		private TroopsSensor troopsSensor;

		private AgentBehaviour agent;
		private GoapActionProvider provider;
		private GoapBehaviour goap;
		private CommanderData commanderData;
		private EnemyBrainData brainData;

		private IGoal previousGoal;

		private void Awake()
		{
			this.goap = FindAnyObjectByType<GoapBehaviour>();
			this.agent = this.GetComponent<AgentBehaviour>();
			this.provider = this.GetComponent<GoapActionProvider>();
			this.commanderData = this.GetComponent<CommanderData>();
			this.brainData = this.GetComponent<EnemyBrainData>();

			this.playerSensor = this.GetComponentInChildren<PlayerSensor>();
			this.troopsSensor = this.GetComponentInChildren<TroopsSensor>();

			if (this.provider.AgentTypeBehaviour == null)
				this.provider.AgentType = this.goap.GetAgentType(EnemyType.Commander.ToString());
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
			provider.RequestGoal<StrategizeGoal>(true);
		}

		private void OnUnitEnter(GameObject unit)
		{
			Debug.Log($"{unit} IN");
			var unitData = unit.GetComponent<EnemyData>();
			commanderData.TroopsToCommand[(int)unitData.EnemyType].Add(unit);
		}

		private void OnUnitExit(GameObject unit)
		{
			Debug.Log($"{unit} OUT");
			var unitData = unit.GetComponent<EnemyData>();
			commanderData.TroopsToCommand[(int)unitData.EnemyType].Remove(unit);
		}

		private void OnPlayerEnter(Transform Player)
		{
		}

		private void OnPlayerExit(Vector2 LastKnownPosition)
		{
		}

		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
	}
}
