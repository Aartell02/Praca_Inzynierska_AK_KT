using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class UnoccupiedAltarsSensor : LocalWorldSensorBase
	{
		public override void Created() { }

		public override void Update() { }

		public override SenseValue Sense(IActionReceiver agent, IComponentReference references)
		{
			int value = 0;
			foreach (var altar in references.GetCachedComponent<EnemyBrainData>().Altars)
				if (altar.Occupied == false) value++;
			return new SenseValue(value);
		}
	}
}
