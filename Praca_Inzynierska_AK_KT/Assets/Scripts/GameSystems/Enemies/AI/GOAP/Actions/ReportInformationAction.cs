using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystems.AI
{
	public class ReportInformationAction : GoapActionBase<ReportInformationAction.Data>, IInjectable
	{
		EnemyConfig enemyConfig;
		public class Data : IActionData
		{
			internal EnemyBrainData brainData;
			public ITarget Target { get; set; }
			public float Timer { get; set; }
		}
		public override void Start(IMonoAgent agent, Data data)
		{
			data.brainData = agent.GetComponent<EnemyBrainData>();
			data.Timer = enemyConfig.EnemyCommunicationData.Delay;
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			data.Timer -= context.DeltaTime;
			if (context.IsInRange)
			{
				float range = enemyConfig.EnemyCommunicationData.SensorRadius;

				Vector3 position = agent.transform.position;

				Collider2D[] hits = Physics2D.OverlapCircleAll(position, range);
				foreach (Collider2D hit in hits)
				{
					CommanderBrainBehaviour commanderData = hit.GetComponent<CommanderBrainBehaviour>();

					if (commanderData != null)
					{
						bool success = false;
						EnemyBrainData brainData = hit.GetComponent<EnemyBrainData>();
						foreach(var altar in data.brainData.Altars)
						{
							if(brainData.AddAltarPosition(altar))
								success = true;
						}

						data.brainData.SetGoal(AIEnemyGoal.None, true);
						if(!success)
							data.brainData.ClearInfo();
						return ActionRunState.Completed;
					}

				}
			}

			return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Stop;
		}

		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
		
	}
}
