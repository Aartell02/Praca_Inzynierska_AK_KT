using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class CommanderCapabilityFactory : CapabilityFactoryBase
	{
		public override ICapabilityConfig Create()
		{
			var builder = new CapabilityBuilder("CommanderCapability");

			BuildGoals(builder);
			BuildActions(builder);
			BuildSensors(builder);

			return builder.Build();
		}

		void BuildGoals(CapabilityBuilder builder)
		{
			builder.AddGoal<StrategizeGoal>()
				.AddCondition<IsPlanning>(Comparison.GreaterThanOrEqual, 1);

			builder.AddGoal<KillPlayerGoal>()
				.AddCondition<PlayerHealth>(Comparison.SmallerThanOrEqual, 0);

		}

		void BuildActions(CapabilityBuilder builder)
		{
			builder.AddAction<Strategize>()
				.AddEffect<IsPlanning>(EffectType.Increase)
				.SetBaseCost(5);

			builder.AddAction<AttackAction>()
				.SetTarget<PlayerTarget>()
				.AddEffect<PlayerHealth>(EffectType.Decrease)
				.SetBaseCost(1);
		}

		void BuildSensors(CapabilityBuilder builder)
		{
			builder.AddTargetSensor<WanderTargetSensor>().SetTarget<WanderTarget>();
			builder.AddTargetSensor<PlayerTargetSensor>().SetTarget<PlayerTarget>();
		}
	}
}
