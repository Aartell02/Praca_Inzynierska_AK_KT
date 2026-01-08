using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using System;
using UnityEngine;

namespace GameSystems.AI
{
	public class GoToPositionAction : GoapActionBase<GoToPositionAction.Data>, IInjectable
	{
		EnemyConfig enemyConfig;

		public class Data : IActionData
		{
			public ITarget Target { get; set; }
			public float Timer { get; set; }
		}

		public override void Start(IMonoAgent agent, Data data)
		{
			data.Timer = enemyConfig.EnemyCommunicationData.Delay;
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			data.Timer -= context.DeltaTime;

			return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Stop;
		}

		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
	}
}
