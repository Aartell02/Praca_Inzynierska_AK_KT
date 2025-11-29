using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class PlayerTargetSensor : LocalTargetSensorBase
	{
		public override void Created() { }
		public override void Update() { }

		public override ITarget Sense(IActionReceiver agent, IComponentReference references, ITarget existingTarget)
		{
				var player = GameObject.FindGameObjectWithTag("Player");
				if (player == null)
					return null;

				return new TransformTarget(player.transform);
		}
	}
}
