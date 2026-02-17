using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class CommanderTargetSensor : LocalTargetSensorBase, IInjectable
	{
		private EnemyConfig enemyConfig;

		public override void Created() { }
		public override void Update() { }

		public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
		{
			float closestDistanceSqr = float.MaxValue;

			foreach (var commander in EnemySharedData.Commanders)
			{
				if (commander == null) continue;

				float distSqr = (commander.transform.position - agent.Transform.position).sqrMagnitude;

				if (distSqr < closestDistanceSqr)
				{
					closestDistanceSqr = distSqr;
					return new TransformTarget(commander.transform);
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
