using Core;
using GameSystems.Config;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

using UnityEngine;

namespace GameSystems
{
	public class PointsAmount : MonoBehaviour
	{
		public static PointsAmount Instance { get; private set; }

		public int Points = 0;

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public void AddPoints(int x)
		{
			Points += x;
		}

		public int GetPoints()
		{
			return Points;
		}
	}
}

