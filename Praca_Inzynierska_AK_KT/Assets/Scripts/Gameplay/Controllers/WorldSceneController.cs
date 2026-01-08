using GameSystems;
using GameSystems.MapGeneration;
using UnityEngine;

namespace Gameplay.Controllers
{
	public class WorldSceneController : MonoBehaviour
	{
		private void Awake()
		{
			GenerateMap();

		}

		private void Start()
		{
			GameSystemsViewModel.BakeNavMesh();
		}

		public static void GenerateMap()
		{
			var generator = DungeonGenerator.FindFirstObjectByType<DungeonGenerator>();
			generator.GenerateDungeon();
		}
	}
}
