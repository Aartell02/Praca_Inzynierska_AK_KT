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
				//data.commandedData.CommanderInRange = true;
			}

			return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Stop;
		}

		public override void Complete(IMonoAgent agent, Data data)
		{
			float range = enemyConfig.EnemyCommunicationData.CommunicationRadius;

			Vector3 position = agent.transform.position;

			Collider2D[] hits = Physics2D.OverlapCircleAll(position, range);
			foreach (Collider2D hit in hits)
			{
				EnemyBrainData brainData = hit.GetComponent<EnemyBrainData>();

				if (brainData != null)
				{
					foreach (var altar in data.brainData.Altars)
						brainData.AddAltarPosition(altar);
				}
			}
		}
		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
		
	}
}
