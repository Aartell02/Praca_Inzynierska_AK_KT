using GameSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class PlayerUIController : MonoBehaviour
	{
		[SerializeField] private Image HealthBar;
		[SerializeField] private TextMeshProUGUI HealthValue;
		[SerializeField] private TextMeshProUGUI Announcement;
		[SerializeField] private Button LevelUpButton;
		private void Update()
		{
			if (GameSystemsViewModel.TryGetPlayerHp(out var player))
			{
				HealthBar.fillAmount = ((float)player.Item1 / (float)player.Item2);
				HealthValue.text = $"{player.Item1}/{player.Item2}";
			}
		}

		public void OnLevelUp()
		{
			Debug.Log("LevelUP");
		}
	}
}
