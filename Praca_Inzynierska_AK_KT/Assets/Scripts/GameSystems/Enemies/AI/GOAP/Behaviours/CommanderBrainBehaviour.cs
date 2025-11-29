using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Runtime;
using UnityEngine;

namespace GameSystems.AI
{
	public class CommanderBrainBehaviour : MonoBehaviour
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
				this.provider.AgentType = this.goap.GetAgentType(EnemyType.Soldier.ToString());
		}

		private void Start()
		{

		}
	}
}
