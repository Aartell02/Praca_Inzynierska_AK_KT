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
			public ITarget Target { get; set; }
			public float Timer { get; set; }
		}

		public override void Start (IMonoAgent agent, Data data)
		{
			data.Timer = enemyConfig.EnemyCommunicationData.Delay;
		}

		public override IActionRunState Perform (IMonoAgent agent, Data data, IActionContext context)
		{
			data.Timer -= context.DeltaTime;

			if (context.IsInRange)
			{
				//data.enemyData.CommanderInRange = true;

			}


			return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Stop;
		}

		public override void Complete(IMonoAgent agent, Data data)
		{
			//data.enemyData.CommanderInRange = false;
		}
		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
	}
}
