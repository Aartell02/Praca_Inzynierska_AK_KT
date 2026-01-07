using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;

namespace GameSystems.AI
{
	public class ReadyToAttackSensor : LocalWorldSensorBase
	{
		public override void Created() { }

		public override void Update() { }

		public override SenseValue Sense(IActionReceiver agent, IComponentReference references)
		{
			return new SenseValue((int)references.GetCachedComponent<EnemyData>().ReadyToAttack);
		}
	}
}
