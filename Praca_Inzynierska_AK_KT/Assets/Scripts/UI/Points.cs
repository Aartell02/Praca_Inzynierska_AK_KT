using GameSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Points : MonoBehaviour
    {
		PointsAmount instance = PointsAmount.Instance;
		[SerializeField] private TextMeshProUGUI pointsText;

        void Start()
        {
			pointsText.text = "Points: " + PointsAmount.Instance.GetPoints().ToString();
		}

		void Update()
		{
			pointsText.text = "Points: " + PointsAmount.Instance.GetPoints().ToString();
		}
    }
}
