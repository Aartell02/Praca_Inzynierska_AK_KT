using System;
using UnityEditor;
using UnityEngine;

namespace Core.Inspector
{
	[CustomPropertyDrawer(typeof(EnumLabelsAttribute))]
	public class EnumLabelsDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EnumLabelsAttribute enumAttr = (EnumLabelsAttribute)attribute;

			if (property.propertyType != SerializedPropertyType.String)
			{
				EditorGUI.LabelField(position, label.text, "EnumLabels can only be used with string fields.");
				return;
			}

			string[] enumNames = Enum.GetNames(enumAttr.EnumType);

			int currentIndex = Array.IndexOf(enumNames, property.stringValue);
			if (currentIndex < 0) currentIndex = 0;

			property.stringValue = enumNames[EditorGUI.Popup(position, label.text, currentIndex, enumNames)];
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}
	}
}
