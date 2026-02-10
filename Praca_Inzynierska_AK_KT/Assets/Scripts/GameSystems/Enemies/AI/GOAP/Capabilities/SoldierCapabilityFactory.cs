using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class SoldierCapabilityFactory : CapabilityFactoryBase, IInjectable
	{
		EnemyConfig enemyConfig;
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
				.AddCondition<ReadyToAttack>(Comparison.GreaterThanOrEqual,1)
				.SetTarget<PlayerTarget>()
				.AddEffect<PlayerHealth>(EffectType.Decrease)
				.SetBaseCost(5)
				.SetStoppingDistance(enemyConfig.EnemyAttackData.MeleeAttackRadius);
		}

		void BuildSensors(CapabilityBuilder builder)
		{
			builder.AddTargetSensor<PlayerTargetSensor>().SetTarget<PlayerTarget>();
			builder.AddWorldSensor<ReadyToAttackSensor>().SetKey<ReadyToAttack>(); 
		}

		public void Inject(DependencyInjector injector)
		{
			this.enemyConfig = injector.EnemyConfig;
		}
	}
}
