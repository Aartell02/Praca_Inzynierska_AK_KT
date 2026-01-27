using Core.Services;
using Gameplay;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSystems
{
	class SkillTreeOption : MonoBehaviour, IPointerClickHandler
	{
		internal Stat _option;
		private SkillTree _parent;
		[SerializeField]
		private TextMeshProUGUI _name;
		public void Initialize(SkillTree parent, Stat option)
		{
			_option = option;
			_parent = parent;
			_name.text = Enum.GetNames(typeof(Stat))[(int)option];
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			_parent.SelectReward(_option, 10);
		}
	}
}
