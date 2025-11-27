using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Core;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class PlayerAliveSensor : LocalWorldSensorBase
	{
		public override void Created() { }
		public override void Update() { }

		public override SenseValue Sense(IActionReceiver agent, IComponentReference references)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			return (player != null);
		}
	}
}
