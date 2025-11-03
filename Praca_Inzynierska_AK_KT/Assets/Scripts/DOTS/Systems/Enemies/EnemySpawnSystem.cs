using Codice.CM.Client.Differences.Graphic;
using Core;
using DOTS.Authoring;
using Unity.Entities;
using static UnityEngine.EventSystems.EventTrigger;

namespace DOTS.Systems.Enemies
{
	[UpdateInGroup(typeof(SimulationSystemGroup))]
	public partial struct EnemySpawnSystem : ISystem
	{
		public void OnCreate(ref SystemState state)
		{
			state.RequireForUpdate<EntityReferences>();
		}

		public void OnUpdate(ref SystemState state)
		{

		}

		public void SpawnEnemy(EnemyType enemyType, int count)
		{

		}
	}
}
