using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class SearchForTargetAction : GoapActionBase<SearchForTargetAction.Data>, IInjectable
	{
		EnemyConfig enemyConfig;
		public class Data : IActionData
		{
			public EnemyBrainData brainData {  get; set; }

			public float Timer;
			public ITarget Target { get; set; }
		}
		public override void Start(IMonoAgent agent, Data data)
		{
			data.brainData = agent.GetComponent<EnemyBrainData>();
			data.Timer = enemyConfig.EnemyAttackData.MeleeAttackDelay;
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			data.Timer -= context.DeltaTime;

			float range = enemyConfig.EnemyCommunicationData.SensorRadius;

			Vector3 position = agent.transform.position;

			Collider2D[] hits = Physics2D.OverlapCircleAll(position, range);
			foreach (Collider2D hit in hits)
			{
				AltarData altarData = hit.GetComponent<AltarData>();

				if (altarData != null) 
					if(data.brainData.AddAltarPosition(hit.gameObject))
						return ActionRunState.Completed;
			}

			return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Stop;
		}

		public override void End(IMonoAgent agent, Data data)
		{
			Debug.Log($"{agent.gameObject.name} stopped attacking");
		}

		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
	}
}
