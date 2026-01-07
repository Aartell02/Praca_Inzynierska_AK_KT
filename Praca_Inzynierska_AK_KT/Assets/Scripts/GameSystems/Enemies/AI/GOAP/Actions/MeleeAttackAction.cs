using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class MeleeAttackAction : GoapActionBase<MeleeAttackAction.Data>, IInjectable
	{
		EnemyConfig enemyConfig;
		public class Data : IActionData
		{
			public EnemyType enemyType;

			public float Timer;
			public ITarget Target { get; set; }
		}
		public override void Start (IMonoAgent agent, Data data)
		{

			var enemyStats = agent.GetComponent<EnemyData>();
			data.enemyType = enemyStats.EnemyType;
			data.Timer = enemyConfig.EnemyAttackData.MeleeAttackDelay;
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			data.Timer -= context.DeltaTime;

			bool shouldAttack = data.Target != null && Vector2.Distance(data.Target.Position, agent.Transform.position) <= enemyConfig.EnemyAttackData.MeleeAttackRadius;

			if (shouldAttack)
			{

			}

			return data.Timer > 0.5f ? ActionRunState.Continue : ActionRunState.Stop;
		}

		public override void End(IMonoAgent agent, Data data)
		{
		}

		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
	}
}
