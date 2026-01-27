using GameSystems;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
	class FloorProgress : MonoBehaviour
	{
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
				Instantiate(PrefabReferences.ExitTrigger, GameSystemsViewModel.GetEnemySpawnPoint(), default);
				Debug.Log("Przejście otwarte!");
			}
		}

	}
}
