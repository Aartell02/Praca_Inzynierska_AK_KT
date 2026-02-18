using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using GameSystems;

public class DungeonPlayModeTests
{
	private IEnumerator LoadGameplay()
	{
		yield return SceneManager.LoadSceneAsync("BootScene");

		while (SceneManager.GetActiveScene().name != "GameplayScene")
			yield return null;

		yield return null;
	}

	[UnityTest]
	public IEnumerator Dungeon_IsGenerated()
	{
		yield return LoadGameplay();

		Assert.IsTrue(GameSystemsViewModel.GetGeneratedRoomCenters().Count > 0);
	}

	[UnityTest]
	public IEnumerator SpawnPoints_AreDifferent()
	{
		yield return LoadGameplay();

		var playerSpawn = GameSystemsViewModel.GetPlayerSpawnPoint();
		var enemySpawn = GameSystemsViewModel.GetEnemySpawnPoint();

		Assert.AreNotEqual(playerSpawn, enemySpawn);
	}

	[UnityTest]
	public IEnumerator ItemsExistOnTilemaps()
	{
		yield return LoadGameplay();

		var walkable = GameObject.Find("WalkableItems");
		var blocking = GameObject.Find("UnwalkableItems");

		Assert.NotNull(walkable);
		Assert.NotNull(blocking);

		var walkMap = walkable.GetComponent<UnityEngine.Tilemaps.Tilemap>();
		var blockMap = blocking.GetComponent<UnityEngine.Tilemaps.Tilemap>();

		Assert.IsTrue(
			walkMap.GetUsedTilesCount() > 0 ||
			blockMap.GetUsedTilesCount() > 0
		);
	}
}
