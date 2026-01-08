using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;
using UnityEngine.AI;

namespace GameSystems.AI
{
	public class UnitPositionTargetSensor : LocalTargetSensorBase
	{
		public override void Created() { }

		public override void Update() { }

		public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget) => new PositionTarget(references.GetCachedComponent<EnemyBrainData>().DefaultPosition);
	}
}
