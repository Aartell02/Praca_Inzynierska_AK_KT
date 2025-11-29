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

			factory.AddCapability<SoldierCapabilityFactory>();

			return factory.Build();
		}
	}
}
