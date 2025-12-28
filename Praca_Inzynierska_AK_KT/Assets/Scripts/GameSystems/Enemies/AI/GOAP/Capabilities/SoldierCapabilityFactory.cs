using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class SoldierCapabilityFactory : CapabilityFactoryBase
	{
		public override ICapabilityConfig Create()
		{
			var builder = new CapabilityBuilder("SoldierCapability");

			BuildGoals(builder);
			BuildActions(builder);
			BuildSensors(builder);

			return builder.Build();
		}

		void BuildGoals(CapabilityBuilder builder)
		{
			builder.AddGoal<KillPlayerGoal>()
				.AddCondition<PlayerHealth>(Comparison.SmallerThanOrEqual, 0);
		}

		void BuildActions(CapabilityBuilder builder)
		{
			builder.AddAction<MeleeAttackAction>()
				.SetMoveMode(ActionMoveMode.MoveBeforePerforming)
				.SetTarget<PlayerTarget>()
				.AddEffect<PlayerHealth>(EffectType.Decrease)
				.SetBaseCost(1);
		}

		void BuildSensors(CapabilityBuilder builder)
		{
			builder.AddTargetSensor<SearchTargetSensor>().SetTarget<WanderTarget>();
			builder.AddTargetSensor<PlayerTargetSensor>().SetTarget<PlayerTarget>();
		}
	}
}
