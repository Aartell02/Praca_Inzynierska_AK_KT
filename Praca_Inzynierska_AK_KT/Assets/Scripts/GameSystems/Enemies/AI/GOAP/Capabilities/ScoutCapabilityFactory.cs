using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class ScoutCapabilityFactory : CapabilityFactoryBase
	{
		public override ICapabilityConfig Create()
		{
			var builder = new CapabilityBuilder("ScoutCapability");

			BuildGoals(builder);
			BuildActions(builder);
			BuildSensors(builder);

			return builder.Build();
		}

		void BuildGoals(CapabilityBuilder builder)
		{
			builder.AddGoal<GetCommandGoal>()
				.AddCondition<HasGoal>(Comparison.GreaterThanOrEqual, 1);
		}

		void BuildActions(CapabilityBuilder builder)
		{
			builder.AddAction<ListenToCommandsAction>()
				.SetTarget<CommanderTarget>()
				.AddEffect<HasGoal>(EffectType.Increase)
				.SetBaseCost(1);
		}

		void BuildSensors(CapabilityBuilder builder)
		{
			builder.AddTargetSensor<CommanderTargetSensor>().SetTarget<CommanderTarget>();
		}
	}
}
