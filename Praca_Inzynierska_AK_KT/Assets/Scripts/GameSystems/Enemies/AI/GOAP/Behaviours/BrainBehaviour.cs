using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.GenTest;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace CrashKonijn.Goap.GenTest
{
	public class BrainBehaviour : MonoBehaviour
	{
		private AgentBehaviour agent;
		private GoapActionProvider provider;
		private GoapBehaviour goap;

		private void Awake()
		{
			this.goap = FindAnyObjectByType<GoapBehaviour>();
			this.agent = this.GetComponent<AgentBehaviour>();
			this.provider = this.GetComponent<GoapActionProvider>();

			// This only applies sto the code demo
			if (this.provider.AgentTypeBehaviour == null)
				this.provider.AgentType = this.goap.GetAgentType("ScriptDemoAgent");
		}

		private void Start()
		{
			this.provider.RequestGoal<IdleGoal>();
		}
	}
}
