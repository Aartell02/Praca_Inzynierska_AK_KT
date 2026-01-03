using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class HasOrderSensor : LocalWorldSensorBase
	{
		public override void Created() { }

		public override void Update() { }

		public override SenseValue Sense(IActionReceiver agent, IComponentReference references)
		{
			return new SenseValue((int)references.GetCachedComponent<EnemyBrainData>().Order);
		}
	}
}
