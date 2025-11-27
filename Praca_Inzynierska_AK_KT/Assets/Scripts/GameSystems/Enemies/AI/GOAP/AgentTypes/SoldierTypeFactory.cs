using Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Goap.GenTest
{
	public class SoldierTypeFactory : AgentTypeFactoryBase
	{
		public override IAgentTypeConfig Create()
		{
			var factory = new AgentTypeBuilder(EnemyType.Soldier.ToString());

			factory.AddCapability<IdleCapabilityFactory>();

			return factory.Build();
		}
	}
}
