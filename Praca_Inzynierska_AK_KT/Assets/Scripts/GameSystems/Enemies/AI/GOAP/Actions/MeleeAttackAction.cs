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

			public EnemyData EnemyStats;
		}
		public override void Start (IMonoAgent agent, Data data)
		{
			var enemyStats = agent.GetComponent<EnemyData>();
			data.EnemyStats = enemyStats;
			data.enemyType = enemyStats.EnemyType;
			data.Timer = 0.2f;
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			data.Timer -= context.DeltaTime;

			float range = enemyConfig.EnemyAttackData.MeleeAttackRadius; ;

			Vector3 position = agent.transform.position;

			Collider2D[] hits = Physics2D.OverlapCircleAll(position, range);
			foreach (Collider2D hit in hits)
				if(hit.GetComponent<PlayerStats>())
					return ActionRunState.Completed;


			return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Stop;
		}

		public override void Complete(IMonoAgent agent, Data data)
		{
			if (data.Target == null) return;
			if (data.EnemyStats.AttackHitboxPrefab == null) return;

			Vector3 agentPos = agent.Transform.position;
			Vector3 targetPos = data.Target.Position;

			Vector2 direction = (targetPos - agentPos).normalized;

			float offset = 1.0f;
			Vector2 spawnPos = (Vector2)agentPos + (direction * offset);

			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

			GameObject hitbox = Object.Instantiate(data.EnemyStats.AttackHitboxPrefab, spawnPos, rotation);

			if (hitbox.TryGetComponent(out MeleeHitbox hitboxScript))
			{
				LayerMask playerLayer = LayerMask.GetMask("Player");
				int damage = 10;

				hitboxScript.Initialize(agent.gameObject, damage, playerLayer, 0.2f);
				data.EnemyStats.ReadyToAttack--;
			}
		}

		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
	}
}
