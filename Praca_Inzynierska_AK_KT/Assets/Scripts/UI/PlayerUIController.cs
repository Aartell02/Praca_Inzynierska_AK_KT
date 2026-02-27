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
		[SerializeField] private Image ExpBar;
		[SerializeField] private TextMeshProUGUI Level;

		private float Timer = 0;
		static float AnnouncementTime = 3;

		private void Update()
		{
			if (GameSystemsViewModel.TryGetPlayerHp(out var player))
			{
				HealthBar.fillAmount = ((float)player.Item1 / (float)player.Item2);
				HealthValue.text = $"{player.Item1}/{player.Item2}";
				ExpBar.fillAmount = PlayerStats.Instance.Experience/100;
				Level.text = $"{PlayerStats.Instance.Level}";
			}
			if (GameSystemsViewModel.TryGetAnnouncement(out var announcement))
			{
				Timer += Time.deltaTime;
				Announcement.text = announcement;
				if (Timer >= AnnouncementTime)
				{
					GameSystemsViewModel.ResetAnnouncement();
					Announcement.text = "";
					Timer = 0;
				}
			}
		}

		public void OnLevelUp()
		{
			Debug.Log("LevelUP");
		}
	}
}
