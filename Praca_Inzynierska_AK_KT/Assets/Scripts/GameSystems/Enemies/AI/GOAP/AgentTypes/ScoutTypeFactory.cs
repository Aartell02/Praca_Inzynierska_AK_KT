using Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class ScoutTypeFactory : AgentTypeFactoryBase
	{
		public override IAgentTypeConfig Create()
		{
			var factory = new AgentTypeBuilder(EnemyType.Scout.ToString());

			factory.AddCapability<ScoutCapabilityFactory>();

			return factory.Build();
		}
	}
}
