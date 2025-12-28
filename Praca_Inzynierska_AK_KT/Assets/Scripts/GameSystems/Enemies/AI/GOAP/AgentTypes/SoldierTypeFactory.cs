using Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class SoldierTypeFactory : AgentTypeFactoryBase
	{
		public override IAgentTypeConfig Create()
		{
			var factory = this.CreateBuilder(EnemyType.Soldier.ToString());

			factory.AddCapability<SoldierCapabilityFactory>();
			factory.AddCapability<UnitCapabilityFactory>();

			return factory.Build();
		}
	}
}
