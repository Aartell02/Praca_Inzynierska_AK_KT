using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boot
{
	[DefaultExecutionOrder(-1000)]
	public class BootManager : MonoBehaviour
	{
		public static BootManager Instance { get; private set; }
		[SerializeField, Tooltip("If true, BootManager won't destroy itself when new scene loads.")]
		private bool persistAcrossScenes = true;

		public event Action OnBootCompleted;
		public event Action<float> OnBootProgress; 
		public event Action<string> OnBootLog;

		void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(gameObject);
				return;
			}
			Instance = this;

			if (persistAcrossScenes)
				DontDestroyOnLoad(gameObject);
		}

		void Start()
		{
			StartCoroutine(BootMainMenu());
		}

		internal void StartGame() => StartCoroutine(StartGameSequenceCoroutine());

		internal void LoadFloor() => StartCoroutine(StartLoadingFloor());

		internal void FinishGame(bool isWon) => StartCoroutine(FinishGameCoroutine());
		private IEnumerator StartGameSequenceCoroutine()
		{
			OnBootLog?.Invoke("BootManager: Starting bootstrap sequence.");

			float overallStart = Time.realtimeSinceStartup;

			SceneManager.UnloadSceneAsync("MainMenuScene");

			OnBootLog?.Invoke($"BootManager: Loading initial scene 'ConfigScene'...");
			yield return StartCoroutine(LoadSceneAsync("ConfigScene"));

			yield return null;

			OnBootLog?.Invoke($"BootManager: Loading initial scene 'GameRunScene'...");
			yield return StartCoroutine(LoadSceneAsync("GameRunScene"));
		}

		internal IEnumerator StartLoadingFloor()
		{
			// --- FAZA 1: SPRZĄTANIE (Unloading) ---
			// Sprawdzamy i usuwamy WorldScene, jeśli już istnieje
			var worldScene = SceneManager.GetSceneByName("WorldScene");
			if (worldScene.IsValid() && worldScene.isLoaded)
			{
				OnBootLog?.Invoke($"BootManager: Unloading existing 'WorldScene'...");
				yield return SceneManager.UnloadSceneAsync("WorldScene");
			}

			// Sprawdzamy i usuwamy GameplayScene, jeśli już istnieje
			var gameplayScene = SceneManager.GetSceneByName("GameplayScene");
			if (gameplayScene.IsValid() && gameplayScene.isLoaded)
			{
				OnBootLog?.Invoke($"BootManager: Unloading existing 'GameplayScene'...");
				yield return SceneManager.UnloadSceneAsync("GameplayScene");
			}

			// Czekamy jedną klatkę, aby silnik przetworzył usunięcie obiektów
			yield return null;

			// --- FAZA 2: ŁADOWANIE (Loading) ---
			// Ładujemy sceny w wymaganej kolejności
			OnBootLog?.Invoke($"BootManager: Loading scene 'WorldScene'...");
			yield return StartCoroutine(LoadSceneAsync("WorldScene"));

			OnBootLog?.Invoke($"BootManager: Loading scene 'GameplayScene'...");
			yield return StartCoroutine(LoadSceneAsync("GameplayScene"));

			// --- FAZA 3: AKTYWACJA ---
			yield return null;

			// Ustawiamy GameplayScene jako aktywną (dla instancjonowania obiektów w niej)
			SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameplayScene"));

			OnBootLog?.Invoke("BootManager: Floor reload sequence completed successfully.");
			OnBootCompleted?.Invoke();
		}


		internal IEnumerator FinishGameCoroutine()
		{
			var gameplayScene = SceneManager.GetSceneByName("GameplayScene");
			if (gameplayScene.IsValid() && gameplayScene.isLoaded)
			{
				OnBootLog?.Invoke($"BootManager: Unloading existing 'GameplayScene'...");
				yield return SceneManager.UnloadSceneAsync("GameplayScene");
			}

			var worldScene = SceneManager.GetSceneByName("WorldScene");
			if (worldScene.IsValid() && worldScene.isLoaded)
			{
				OnBootLog?.Invoke($"BootManager: Unloading existing 'WorldScene'...");
				yield return SceneManager.UnloadSceneAsync("WorldScene");
			}

			var gameRunScene = SceneManager.GetSceneByName("GameRunScene");
			if (gameRunScene.IsValid() && gameRunScene.isLoaded)
			{
				OnBootLog?.Invoke($"BootManager: Unloading existing 'GameRunScene'...");
				yield return SceneManager.UnloadSceneAsync("GameRunScene");
			}

			var configScene = SceneManager.GetSceneByName("ConfigScene");
			if (configScene.IsValid() && configScene.isLoaded)
			{
				OnBootLog?.Invoke($"BootManager: Unloading existing 'ConfigScene'...");
				yield return SceneManager.UnloadSceneAsync("ConfigScene");
			}

			// Czekamy jedną klatkę, aby silnik przetworzył usunięcie obiektów
			yield return null;

			// --- FAZA 2: ŁADOWANIE (Loading) ---
			// Ładujemy sceny w wymaganej kolejności
			OnBootLog?.Invoke($"BootManager: Loading scene 'WorldScene'...");
			yield return StartCoroutine(LoadSceneAsync("MainMenuScene"));
			// --- FAZA 3: AKTYWACJA ---
			yield return null;

			// Ustawiamy GameplayScene jako aktywną (dla instancjonowania obiektów w niej)
			SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenuScene"));

			OnBootLog?.Invoke("BootManager: Floor reload sequence completed successfully.");
			OnBootCompleted?.Invoke();
		}

		private IEnumerator BootMainMenu()
		{
			OnBootLog?.Invoke($"BootManager: Loading initial scene 'MainMenu'");
			yield return StartCoroutine(LoadSceneAsync("MainMenuScene"));

			SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenuScene"));

			OnBootLog?.Invoke("BootManager: Boot sequence completed successfully.");
			OnBootCompleted?.Invoke();
		}

		internal IEnumerator LoadSceneAsync(string scene)

		{
			var aso = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
			if (aso == null)
			{
				Debug.LogError($"BootManager: Scene '{scene}' not found or failed to start loading.");
				yield break;
			}
			while (!aso.isDone)
			{
				OnBootProgress?.Invoke(0.9f + aso.progress * 0.1f);
				yield return null;
			}
			yield return null;
		}
	}
}
