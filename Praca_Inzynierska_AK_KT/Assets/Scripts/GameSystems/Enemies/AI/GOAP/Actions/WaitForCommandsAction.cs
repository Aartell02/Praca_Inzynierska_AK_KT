using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using System;
using UnityEngine;

namespace GameSystems.AI
{
	public class WaitForCommandsAction : GoapActionBase<WaitForCommandsAction.Data>, IInjectable
	{
		EnemyConfig enemyConfig;

		public class Data : IActionData
		{
			internal EnemyData enemyData;
			public ITarget Target { get; set; }
		}

		public override void Start (IMonoAgent agent, Data data)
		{
			data.enemyData = agent.GetComponent<EnemyData>();
		}

		public override IActionRunState Perform (IMonoAgent agent, Data data, IActionContext context)
		{

			Debug.Log($"{agent.gameObject.name} in range");

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
