using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Inspector;

namespace Boot
{
	[DefaultExecutionOrder(-1000)]
	public class BootManager : MonoBehaviour
	{
		public static BootManager Instance { get; private set; }

		[Header("Boot Configuration")]
		[EnumArray(typeof(GameState))]
		public string[] scenes;

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
			StartCoroutine(BootSequenceCoroutine());
		}

		private IEnumerator BootSequenceCoroutine()
		{
			OnBootLog?.Invoke("BootManager: Starting bootstrap sequence.");

			float overallStart = Time.realtimeSinceStartup;
			if (scenes is not null)
			{
				OnBootLog?.Invoke($"BootManager: Loading initial scene '{scenes[0]}'...");
				yield return StartCoroutine(LoadSceneAsync(scenes[(int)GameState.Gameplay]));
			}
			OnBootLog?.Invoke("BootManager: Boot sequence completed successfully.");
			OnBootCompleted?.Invoke();
		}

		private IEnumerator LoadSceneAsync(string scene)
		{
			var aso = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
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
