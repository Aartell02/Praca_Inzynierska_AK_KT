using System;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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

		private IEnumerator StartGameSequenceCoroutine()
		{
			OnBootLog?.Invoke("BootManager: Starting bootstrap sequence.");

			float overallStart = Time.realtimeSinceStartup;

			OnBootLog?.Invoke($"BootManager: Loading initial scene 'ConfigScene'...");
			yield return StartCoroutine(LoadSceneAsync("ConfigScene"));

			OnBootLog?.Invoke($"BootManager: Loading initial scene 'WorldScene'...");
			yield return StartCoroutine(LoadSceneAsync("WorldScene"));

			OnBootLog?.Invoke($"BootManager: Loading initial scene 'GameplayScene'...");
			yield return StartCoroutine(LoadSceneAsync("GameplayScene"));

			SceneManager.UnloadSceneAsync("MainMenuScene");

			SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameplayScene"));

			OnBootLog?.Invoke("BootManager: Boot sequence completed successfully.");
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
