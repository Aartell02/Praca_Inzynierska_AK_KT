using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystems.AI
{
	public class SendScoutsAction : GoapActionBase<SendScoutsAction.Data>, IInjectable
	{
		EnemyConfig enemyConfig;
		public class Data : IActionData
		{
			public float Timer {  get; set; }
			public ITarget Target { get; set; }
		}
		public override void Start(IMonoAgent agent, Data data)
		{

			data.Timer = enemyConfig.EnemyCommunicationData.Delay;
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			data.Timer -= context.DeltaTime;

			return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Completed;
		}

		public override void Complete(IMonoAgent agent, Data data)
		{
			float range = enemyConfig.EnemyCommunicationData.CommunicationRadius;

			Vector3 position = agent.transform.position;

			Collider2D[] hits = Physics2D.OverlapCircleAll(position, range);
			foreach (Collider2D hit in hits)
			{
				ScoutBrainBehaviour scoutData = hit.GetComponent<ScoutBrainBehaviour>();

				if (scoutData != null)
				{
					scoutData.SetOrder(AIEnemyOrder.Scout);
				}
			}
		}

		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
	}
}
