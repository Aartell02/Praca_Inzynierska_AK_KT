using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using log4net.Util;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.AI
{
	public class CommanderTargetSensor : LocalTargetSensorBase, IInjectable
	{
		private EnemyConfig enemyConfig;

		private List<Collider2D> Results = new();

		public override void Created() { }
		public override void Update() { }

		public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
		{
			float closestDistanceSqr = float.MaxValue;

			foreach (var commander in EnemySharedData.Commanders)
			{
				if (commander == null) continue;

				float distSqr = (commander.position - agent.Transform.position).sqrMagnitude;

				if (distSqr < closestDistanceSqr)
				{
					closestDistanceSqr = distSqr;
					return new PositionTarget(commander.position);
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
