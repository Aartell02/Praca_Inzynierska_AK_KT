using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.GenTest;
using CrashKonijn.Goap.Runtime;

namespace CrashKonijn.Goap.GenTest
{
	public class DemoAgentTypeFactory : AgentTypeFactoryBase
	{
		public override IAgentTypeConfig Create()
		{
			var factory = new AgentTypeBuilder("ScriptDemoAgent");

			factory.AddCapability<IdleCapabilityFactory>();

			return factory.Build();
		}
	}
}
