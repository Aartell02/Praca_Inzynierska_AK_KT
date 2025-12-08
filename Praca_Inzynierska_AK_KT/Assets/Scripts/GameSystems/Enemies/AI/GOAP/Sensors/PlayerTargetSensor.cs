using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.AI
{
	public class PlayerTargetSensor : LocalTargetSensorBase, IInjectable
	{
		private EnemyConfig enemyConfig;

		private List<Collider2D> Results = new();

		public override void Created() { }
		public override void Update() { }

		public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
		{
			var enemyStats = references.GetCachedComponentInParent<EnemyData>();

			int hitCount = Physics2D.OverlapCircle(
				agent.Transform.position,
				enemyConfig.EnemyData[(int)enemyStats.EnemyType].SensorRadius,
				ContactFilter2D.noFilter,
				Results
			);

			for (int i = 0; i < hitCount; i++)
			{
				if (Results[i].CompareTag("Player"))
				{
					return new TransformTarget(Results[i].transform);
				}
			}

			return null;
		}
		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
	}
}
