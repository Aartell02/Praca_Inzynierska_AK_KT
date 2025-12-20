using Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class CommanderTypeFactory : AgentTypeFactoryBase
	{
		public override IAgentTypeConfig Create()
		{
			var factory = this.CreateBuilder(EnemyType.Commander.ToString());

			factory.AddCapability<CommanderCapabilityFactory>();

			return factory.Build();
		}
	}
}
