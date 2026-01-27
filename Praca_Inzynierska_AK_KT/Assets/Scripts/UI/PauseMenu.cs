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
		public GameObject Body;

		private void Start()
		{
			Resume.onClick.AddListener(OnButtonResume);
			Quit.onClick.AddListener(OnButtonQuit);
			Body.SetActive(false);
		}

		public void PauseGame()
		{
			Body.SetActive(true);
			GameRunState.PauseGame();
		}

		public void ResumeGame()
		{
			GameRunState.ResumeGame();
			Body.SetActive(false);
		}

		private void OnButtonResume() => ResumeGame();

		private void OnButtonQuit()
		{
			GameRunState.FinishGame(false);
		}
	}
}
