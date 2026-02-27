using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class MainMenu : MonoBehaviour
	{
		public Button Play;
		public Button Quit;

		private void Start()
		{
			Play.onClick.AddListener(OnButtonPlay);
			Quit.onClick.AddListener(OnButtonQuit);
		}

		private void OnButtonPlay() => GameRunState.StartGame();

		private void OnButtonQuit()
		{
			Application.Quit();

#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}
	}
}
