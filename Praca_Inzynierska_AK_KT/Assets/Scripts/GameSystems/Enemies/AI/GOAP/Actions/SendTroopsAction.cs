using Core;
using CrashKonijn.Agent.Core;
using CrashKonijn.Goap.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSystems.AI
{
	public class SendTroopsAction : GoapActionBase<SendTroopsAction.Data>, IInjectable
	{
		EnemyConfig enemyConfig;
		public class Data : IActionData
		{
			public CommanderData commanderData { get; set; }
			public List<Collider2D> group {  get; set; }
			public float Timer { get; set; }
			public ITarget Target { get; set; }
			public int RemainingScouts { get; set; }
			public int RemainingSoldiers { get; set; }
		}
		public override void Start(IMonoAgent agent, Data data)
		{
			data.commanderData = agent.GetComponent<CommanderData>();
			data.Timer = enemyConfig.EnemyCommunicationData.Delay;
			data.RemainingSoldiers = enemyConfig.EnemyGroupData.SoldiersCount;
			data.RemainingScouts = enemyConfig.EnemyGroupData.ScoutsCount;
			data.group = new();
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			data.Timer -= context.DeltaTime;

			float range = enemyConfig.EnemyCommunicationData.CommunicationRadius;

			Vector3 position = agent.transform.position;
			var commanderBrainData = agent.GetComponent<EnemyBrainData>();

			foreach(var altar in commanderBrainData.Altars)
			{
				if(altar.gameObject.GetComponent<AltarData>().Occupied)
					continue;
				Collider2D[] hits = Physics2D.OverlapCircleAll(position, range);
				foreach (Collider2D hit in hits)
				{
					if(data.RemainingSoldiers < 1)
						return ActionRunState.WaitThenComplete(10);
					EnemyBrainData brainData = hit.GetComponent<EnemyBrainData>();
					SoldierBrainBehaviour soldierBrainData = hit.GetComponent<SoldierBrainBehaviour>();

					if (soldierBrainData != null)
					{
						brainData.SetDeufaultPosition(altar.transform.position);
						soldierBrainData.SetOrder(AIEnemyOrder.Guard);
						data.RemainingSoldiers--;
					}
					ScoutBrainBehaviour scoutBrainData = hit.GetComponent<ScoutBrainBehaviour>();

					if (scoutBrainData != null)
					{
						brainData.SetDeufaultPosition(altar.transform.position);
						scoutBrainData.SetOrder(AIEnemyOrder.Guard);
						data.RemainingScouts--;
					}
				}
				altar.gameObject.GetComponent<AltarData>().Occupied = true;
			}
			return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Completed;
		}

		public override void Complete(IMonoAgent agent, Data data)
		{

		}

		public void Inject(DependencyInjector injector)
		{
			enemyConfig = injector.EnemyConfig;
		}
	}
}
