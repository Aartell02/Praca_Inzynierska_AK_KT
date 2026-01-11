

using System.Collections.Generic;
using UnityEngine;

namespace GameSystems
{
    public static class GameSystemsViewModel
    {
		static Vector2 EnemySpawnPoint;
		static Vector2 PlayerSpawnPoint;
		static Vector2 TopRoomCenter;
		static Vector2 BottomRoomCenter;
		static List<Vector2> GeneratedRoomCenters = new List<Vector2>();

		public static void SetSpawnPoints(Vector2 playerSpawnPoint, Vector2 enemySpawnPoint)
		{
			PlayerSpawnPoint = playerSpawnPoint;
			EnemySpawnPoint = enemySpawnPoint;
		}

		public static void  SetRoomCenters(Vector2 topRoomCenter, Vector2 bottomRoomCenter)
		{
			TopRoomCenter = topRoomCenter;
			BottomRoomCenter = bottomRoomCenter;
		}

		public static void SetGeneratedRoomCenters(List<Vector2> centers)
		{
			GeneratedRoomCenters = centers;
		}

		public static Vector2 GetPlayerSpawnPoint() => PlayerSpawnPoint;
		public static Vector2 GetEnemySpawnPoint() => EnemySpawnPoint;

		public static Vector2 GetTopRoomCenter() => TopRoomCenter;

		public static Vector2 GetBottomRoomCenter() => BottomRoomCenter;

		public static List<Vector2> GetGeneratedRoomCenters() => GeneratedRoomCenters;

		public static void BakeNavMesh() => NavigationService.BakeNavMesh();

		public static bool TryGetPlayerHp(out (int current, int max) hp)
		{
			var player = GameObject.FindFirstObjectByType<PlayerStats>();
			if (player == null)
			{
				hp = default;
				return false;
			}

			var lifeState = player.GetComponent<LifeStateData>();
			if (lifeState == null)
			{
				hp = default;
				return false;
			}

			hp = (lifeState.Health, player.Health);
			return true;
		}
	}
}

