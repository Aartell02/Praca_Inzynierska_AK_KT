using Core.Services;
using Gameplay;
using GameSystems.Config;
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
		internal StatType Stat;
		internal float Value;
		private SkillTree _parent;

		[SerializeField]
		private TextMeshProUGUI _name;
		[SerializeField]
		private TextMeshProUGUI _value;

		public void Initialize(SkillTree parent, StatType option)
		{
			Stat = option;
			_parent = parent;
			_name.text = Enum.GetNames(typeof(StatType))[(int)option];
			Value = LevelUpService.GenerateValue(Stat);
			_value.text = Value.ToString("F2");
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			_parent.SelectReward(Stat, Value);
		}


	}
}
