using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class HasGoalSensor : LocalWorldSensorBase
	{
		public override void Created() { }

		public override void Update() { }

		public override SenseValue Sense(IActionReceiver agent, IComponentReference references)
		{
			return new SenseValue((int)references.GetCachedComponent<EnemyData>().Goal);
		}
	}

	public class TargetInRangeSensor : LocalWorldSensorBase
	{
		public override void Created() { }

		public override void Update() { }

		public override SenseValue Sense(IActionReceiver agent, IComponentReference references)
		{
			return new SenseValue(references.GetCachedComponent<EnemyData>().TargetInRange);
		}
	}
}
