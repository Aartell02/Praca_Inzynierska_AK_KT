using Core;
using GameSystems;
using GameSystems.MapGeneration;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

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
