using Core.Services;
using Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems
{
	class SkillTree : MonoBehaviour
	{
		public SkillTreeOption[] Options;

		private void Start()
		{
		}

		public void PauseGame()
		{
			GameRunState.PauseGame();
			PlayerInputService._inputActions.Player.Disable();
			PlayerInputService._inputActions.UI.Enable();
		}

		public void ResumeGame()
		{
			GameRunState.ResumeGame();
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
