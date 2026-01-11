using Core.Services;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems
{
	class PauseMenu : MonoBehaviour
	{
		public Button Resume;
		public Button Quit;
		public GameObject Menu;

		private void Start()
		{
			Resume.onClick.AddListener(OnButtonResume);
			Quit.onClick.AddListener(OnButtonQuit);
			Menu.SetActive(false);
		}

		public void PauseGame()
		{
			Menu.SetActive(true);
			GameRunState.PauseGame();
			PlayerInputService._inputActions.Player.Disable();
			PlayerInputService._inputActions.UI.Enable();
		}

		public void ResumeGame()
		{
			GameRunState.ResumeGame();
			Menu.SetActive(false);
			PlayerInputService._inputActions.Player.Enable();
			PlayerInputService._inputActions.UI.Disable();
		}

		private void OnButtonResume() => GameRunState.ResumeGame();

		private void OnButtonQuit()
		{
			GameRunState.FinishGame(false);
		}
	}
}
