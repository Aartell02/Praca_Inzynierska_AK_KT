using Core.Services;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems
{
	class EndgameScreen : MonoBehaviour
	{
		public GameObject Body;
		public Button Return;
		[SerializeField] public TextMeshProUGUI Points;

		private void Start()
		{
			Return.onClick.AddListener(OnButtonReturn);
			Body.SetActive(false);
		}

		private void OnEnable()
		{
			LifeStateData.OnGameOver += FinishGame;
		}

		private void OnDisable()
		{
			LifeStateData.OnGameOver -= FinishGame;
		}

		public void FinishGame()
		{			
			GameRunState.PauseGame();
			Points.text = "Points: " + PointsAmount.Instance.GetPoints().ToString();
			Body.SetActive(true);
		}

		private void OnButtonReturn()
		{
			GameRunState.FinishGame(false);
		}
	}
}
