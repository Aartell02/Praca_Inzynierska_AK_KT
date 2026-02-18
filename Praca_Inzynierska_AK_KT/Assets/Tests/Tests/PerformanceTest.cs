using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Unity.PerformanceTesting;
using System.Collections;
using GameSystems;
using GameSystems.MapGeneration;

public class PerformanceTests
{
	private IEnumerator LoadGameplay()
	{
		yield return SceneManager.LoadSceneAsync("BootScene");

		while (SceneManager.GetActiveScene().name != "GameplayScene")
			yield return null;

		yield return null;
	}

	// =============================
	// Dungeon generation time
	// =============================
	[UnityTest, Performance]
	public IEnumerator Dungeon_Generation_Performance()
	{
		yield return LoadGameplay();

		var gen = Object.FindFirstObjectByType<DungeonGenerator>();
		Assert.NotNull(gen);

		Measure.Method(() =>
		{
			gen.GenerateDungeon();
		})
		.WarmupCount(3)
		.MeasurementCount(10)
		.Run();

		yield return null;
	}

	// =============================
	// Gameplay FPS
	// =============================
	[UnityTest, Performance]
	public IEnumerator Gameplay_FPS()
	{
		yield return LoadGameplay();

		yield return Measure.Frames()
			.WarmupCount(30)
			.MeasurementCount(120)
			.Run();
	}

	// =============================
	// GC allocations
	// =============================
	[UnityTest, Performance]
	public IEnumerator Dungeon_Generation_GC_Only()
	{
		yield return LoadGameplay();

		var gen = Object.FindFirstObjectByType<DungeonGenerator>();
		Assert.NotNull(gen);

		var group = new SampleGroup("GC Allocated", SampleUnit.Byte);

		for (int i = 0; i < 10; i++)
		{
			System.GC.Collect();
			System.GC.WaitForPendingFinalizers();

			long before = System.GC.GetTotalMemory(false);

			gen.GenerateDungeon();

			long after = System.GC.GetTotalMemory(false);

			Measure.Custom(group, after - before);

			yield return null;
		}
	}
}
