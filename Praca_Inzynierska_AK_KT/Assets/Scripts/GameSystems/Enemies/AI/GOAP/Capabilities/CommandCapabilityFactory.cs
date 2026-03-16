using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class CommandCapabilityFactory : CapabilityFactoryBase
	{
		public override ICapabilityConfig Create()
		{
			var builder = new CapabilityBuilder("CommandCapability");

			BuildGoals(builder);
			BuildActions(builder);
			BuildSensors(builder);

			return builder.Build();
		}

		void BuildGoals(CapabilityBuilder builder)
		{
			builder.AddGoal<StrategizeGoal>()
				.AddCondition<IsPlanning>(Comparison.GreaterThanOrEqual, 1)
				.AddCondition<InfoPoints>(Comparison.GreaterThanOrEqual, 1)
				.AddCondition<MapDiscovered>(Comparison.GreaterThanOrEqual, 80)
				.AddCondition<UnoccupiedAltars>(Comparison.SmallerThan, 1);
		}

		void BuildActions(CapabilityBuilder builder)
		{
			builder.AddAction<StrategizeAction>()
				.AddEffect<IsPlanning>(EffectType.Increase)
				.SetBaseCost(30)
				.SetRequiresTarget(false);

			builder.AddAction<SendTroopsAction>()
				.AddCondition<SoldiersToCommand>(Comparison.GreaterThanOrEqual, 2)
				.AddCondition<InfoPoints>(Comparison.GreaterThanOrEqual, 1)
				.AddCondition<MapDiscovered>(Comparison.GreaterThanOrEqual, 40)
				.AddCondition<UnoccupiedAltars>(Comparison.GreaterThanOrEqual, 1)
				.AddEffect<UnoccupiedAltars>(EffectType.Decrease)
				.AddEffect<IsPlanning>(EffectType.Increase)
				.AddEffect<SoldiersToCommand>(EffectType.Decrease)
				.AddEffect<ScoutsToCommand>(EffectType.Decrease)
				.SetBaseCost(5)
				.SetRequiresTarget(false);

			builder.AddAction<SendScoutsAction>()
				.AddCondition<ScoutsToCommand>(Comparison.GreaterThanOrEqual,2)
				.AddCondition<MapDiscovered>(Comparison.SmallerThanOrEqual, 90)
				.AddEffect<IsPlanning>(EffectType.Increase)
				.AddEffect<MapDiscovered>(EffectType.Increase)
				.AddEffect<ScoutsToCommand>(EffectType.Decrease)
				.SetBaseCost(5)
				.SetRequiresTarget(false);
		}

		void BuildSensors(CapabilityBuilder builder)
		{
			builder.AddWorldSensor<SoldiersToCommandSensor>().SetKey<SoldiersToCommand>();
			builder.AddWorldSensor<ScoutsToCommandSensor>().SetKey<ScoutsToCommand>();
			builder.AddWorldSensor<InfoPointsSensor>().SetKey<InfoPoints>();
			builder.AddWorldSensor<MapDiscoveredSensor>().SetKey<MapDiscovered>();
			builder.AddWorldSensor<UnoccupiedAltarsSensor>().SetKey<UnoccupiedAltars>();
		}
	}
}
