using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class TroopsToCommandSensor : LocalWorldSensorBase
	{
		public override void Created() { }

		public override void Update() { }

		public override SenseValue Sense(IActionReceiver agent, IComponentReference references)
		{
			int sum = 0;
			var commanderData = references.GetCachedComponent<CommanderData>().TroopsToCommand;
			foreach (var troops in commanderData)
			{
				sum += troops.Count;
			}
			return new SenseValue(sum);
		}
	}
}
