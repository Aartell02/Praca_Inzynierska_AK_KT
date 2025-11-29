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
				.AddCondition<PlayerVisible>(Comparison.SmallerThanOrEqual, 0)
				.SetBaseCost(1f);

			builder.AddTargetSensor<IdleTargetSensor>()
				.SetTarget<IdleTarget>();

			builder.AddAction<IdleAction>()
				.AddEffect<PlayerVisible>(EffectType.Decrease)
				.SetTarget<IdleTarget>()
				.SetBaseCost(1f);

			return builder.Build();
		}
	}
}
