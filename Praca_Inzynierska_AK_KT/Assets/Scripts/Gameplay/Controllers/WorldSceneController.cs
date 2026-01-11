using GameSystems;
using GameSystems.MapGeneration;
using UnityEngine;

namespace Gameplay.Controllers
{
	public class WorldSceneController : MonoBehaviour
	{
		[SerializeField] GameObject _Structures;
		PrefabReferences gameObjectReferences;
		private void Awake()
		{
			gameObjectReferences = PrefabReferences.Instance;
			GenerateMap();
		}

		private void Start()
		{
			SpawnAltars();
			GameSystemsViewModel.BakeNavMesh();
		}

		public void GenerateMap()
		{
			var generator = DungeonGenerator.FindFirstObjectByType<DungeonGenerator>();
			generator.GenerateDungeon();
		}
		public void SpawnAltars()
		{
			var spawnPoints = GameSystemsViewModel.GetGeneratedRoomCenters();
			foreach (var spawnPoint in spawnPoints)
				Instantiate(gameObjectReferences.Altar, spawnPoint, Quaternion.identity, _Structures.transform);
		}

	}
}
