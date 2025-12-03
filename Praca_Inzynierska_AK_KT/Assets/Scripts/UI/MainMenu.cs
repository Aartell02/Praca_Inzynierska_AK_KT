using Boot;
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

		private void OnButtonPlay()
		{
			Debug.Log("Play clicked");
			BootViewModel.StartGame();
		}

		private void OnButtonQuit()
		{
			Debug.Log("Quit clicked");
			Application.Quit();

			UnityEditor.EditorApplication.isPlaying = false;
		}
	}
}
