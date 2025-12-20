using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class WanderToTargetAction : GoapActionBase<WanderToTargetAction.Data>, IInjectable
	{
		EnemyConfig enemyConfig;

		public class Data : IActionData
		{
			internal EnemyData enemyData;
			public ITarget Target { get; set; }
		}

		public override void Start(IMonoAgent agent, Data data)
		{
			Debug.Log($"{agent.gameObject.name} Searching for commander");

			data.enemyData = agent.GetComponent<EnemyData>();
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			if (context.IsInRange)
			{
				data.enemyData.TargetInRange = true;
				return ActionRunState.Completed;
			}

			return ActionRunState.ContinueOrResolve;
		}

		public override void End(IMonoAgent agent, Data data)
		{
			Debug.Log($"{agent.gameObject.name} stopped listening");
		}

		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
	}
}
