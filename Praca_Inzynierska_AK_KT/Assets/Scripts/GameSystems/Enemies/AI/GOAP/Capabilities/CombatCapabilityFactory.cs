using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class CombatCapabilityFactory : CapabilityFactoryBase
	{
		public override ICapabilityConfig Create()
		{
			var builder = new CapabilityBuilder("CombatCapability");

			// Cel: zabić gracza (czyli PlayerAlive == 0)
			builder.AddGoal<KillPlayerGoal>()
				   .AddCondition<PlayerVisible>(Comparison.GreaterThanOrEqual, 1)
				   .AddCondition<PlayerInRange>(Comparison.SmallerThanOrEqual, 0)
				   .SetBaseCost(1);

			builder.AddGoal<SurviveGoal>()
				   .AddCondition<PlayerVisible>(Comparison.GreaterThanOrEqual, 1)
				   .SetBaseCost(1);

			// Configure Chase Action
			builder.AddAction<ChaseAction>()
				.SetTarget<PlayerTarget>()
				.AddCondition<PlayerVisible>(Comparison.GreaterThanOrEqual, 1)
				.AddEffect<PlayerInRange>(EffectType.Increase)
				.SetBaseCost(2)
				.SetStoppingDistance(2);

			// Configure Attack Action
			builder.AddAction<AttackAction>()
				.SetTarget<PlayerTarget>()
				.AddCondition<PlayerInRange>(Comparison.GreaterThanOrEqual, 1)
				.AddEffect<PlayerInRange>(EffectType.Increase)
				.SetBaseCost(2)
				.SetStoppingDistance(1.5f);

			// Sensory dla zdolności
			builder.AddWorldSensor<PlayerVisibleSensor>().SetKey<PlayerVisible>();
			builder.AddWorldSensor<PlayerInRangeSensor>().SetKey<PlayerInRange>();
			builder.AddTargetSensor<PlayerTargetSensor>().SetTarget<PlayerTarget>();

			return builder.Build();
		}
	}
}
