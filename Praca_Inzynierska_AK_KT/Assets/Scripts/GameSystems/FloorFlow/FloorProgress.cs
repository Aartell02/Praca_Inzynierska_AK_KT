using GameSystems;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
	class FloorProgress : MonoBehaviour
	{
		bool IsCompleted = false;
		int TotalAltarCount;
		int CapturedAltarCount;
		PrefabReferences PrefabReferences;
		public void Start()
		{
			TotalAltarCount = FindObjectsByType<AltarData>(FindObjectsSortMode.None).Count();
			PrefabReferences = FindFirstObjectByType<PrefabReferences>();
		}

		public void OnAltarCaptured()
		{
			CapturedAltarCount++;
			if (CapturedAltarCount >= TotalAltarCount)
			{
				IsCompleted = true;
				Instantiate(PrefabReferences.ExitTrigger, GameSystemsViewModel.GetEnemySpawnPoint(), default);
				Debug.Log("Przejście otwarte!");
			}
		}

	}
}
