using Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class SoldierTypeFactory : AgentTypeFactoryBase
	{
		public override IAgentTypeConfig Create()
		{
			var factory = new AgentTypeBuilder(EnemyType.Soldier.ToString());

			factory.AddCapability<IdleCapabilityFactory>();
			factory.AddCapability<CombatCapabilityFactory>();

			return factory.Build();
		}
	}
}
