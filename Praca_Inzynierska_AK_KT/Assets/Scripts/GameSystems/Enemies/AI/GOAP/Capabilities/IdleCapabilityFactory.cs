using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class IdleCapabilityFactory : CapabilityFactoryBase
	{
		public override ICapabilityConfig Create()
		{
			var builder = new CapabilityBuilder(AIEnemyState.Idle.ToString() + "Capability");

			builder.AddGoal<IdleGoal>()
				.AddCondition<IsIdle>(Comparison.GreaterThanOrEqual, 1)
				.AddCondition<PlayerVisible>(Comparison.SmallerThan, 1)
				.SetBaseCost(50);

			builder.AddTargetSensor<IdleTargetSensor>()
				.SetTarget<IdleTarget>();

			builder.AddAction<IdleAction>()
				.AddEffect<IsIdle>(EffectType.Increase)
				.SetTarget<IdleTarget>();



			return builder.Build();
		}
	}
}
