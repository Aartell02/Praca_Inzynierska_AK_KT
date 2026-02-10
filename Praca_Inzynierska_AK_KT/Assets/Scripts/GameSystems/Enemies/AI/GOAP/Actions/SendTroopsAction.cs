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
			public CommanderBrainBehaviour commanderData { get; set; }
			public EnemyBrainData commanderBrainData { get; set; }
			public List<Collider2D> group {  get; set; }
			public float Timer { get; set; }
			public ITarget Target { get; set; }
			public int RemainingScouts { get; set; }
			public int RemainingSoldiers { get; set; }
		}
		public override void Start(IMonoAgent agent, Data data)
		{
			data.commanderData = agent.GetComponent<CommanderBrainBehaviour>();
			data.Timer = enemyConfig.EnemyCommunicationData.Delay;
			data.commanderBrainData = agent.GetComponent<EnemyBrainData>();
			data.RemainingSoldiers = enemyConfig.EnemyGroupData.SoldiersCount;
			data.RemainingScouts = enemyConfig.EnemyGroupData.ScoutsCount;
			data.group = new();
		}

		public override IActionRunState Perform(IMonoAgent agent, Data data, IActionContext context)
		{
			data.Timer -= context.DeltaTime;
			if (data.RemainingSoldiers < 1)
				return ActionRunState.WaitThenComplete(10);

			float range = enemyConfig.EnemyCommunicationData.CommunicationRadius;

			Vector3 position = agent.transform.position;

			for (int i =0; i< data.commanderBrainData.Altars.Count; i++)
			{
				var altar = data.commanderBrainData.Altars[i];
				if (altar.Occupied)
					continue;

				Collider2D[] hits = Physics2D.OverlapCircleAll(position, range);
				foreach (Collider2D hit in hits)
				{
					if (data.RemainingSoldiers < 1)
						break;
					EnemyBrainData brainData = hit.GetComponent<EnemyBrainData>();
					SoldierBrainBehaviour soldierBrainData = hit.GetComponent<SoldierBrainBehaviour>();

					if (soldierBrainData != null)
					{
						brainData.SetDeufaultPosition(altar.Position);
						soldierBrainData.GiveOrder(AIEnemyGoal.Guard);
						data.RemainingSoldiers--;
					}
					ScoutBrainBehaviour scoutBrainData = hit.GetComponent<ScoutBrainBehaviour>();

					if (scoutBrainData != null)
					{
						brainData.SetDeufaultPosition(altar.Position);
						scoutBrainData.GiveOrder(AIEnemyGoal.Guard);
						data.RemainingScouts--;
					}
				}
				altar.Occupied = true;
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
