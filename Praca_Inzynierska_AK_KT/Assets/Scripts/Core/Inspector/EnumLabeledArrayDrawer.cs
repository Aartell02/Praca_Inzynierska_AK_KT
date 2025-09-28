using System;
using UnityEditor;
using UnityEngine;

namespace Core.Inspector
{
	[CustomPropertyDrawer(typeof(EnumLabeledArrayAttribute))]
	public class EnumLabeledArrayDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EnumLabeledArrayAttribute enumAttr = (EnumLabeledArrayAttribute)attribute;
			string[] enumNames = Enum.GetNames(enumAttr.EnumType);

			// jeśli to element tablicy (Unity daje nazwy w stylu data[0], data[1], …)
			if (property.propertyPath.Contains("Array.data["))
			{
				int index = GetIndexFromPropertyPath(property.propertyPath);
				if (index >= 0 && index < enumNames.Length)
				{
					label.text = enumNames[index]; // ← podmieniamy "Element X"
				}
			}

			EditorGUI.PropertyField(position, property, label, true);
		}

		private int GetIndexFromPropertyPath(string path)
		{
			// np. "statValues.Array.data[2]"
			int start = path.IndexOf("[") + 1;
			int end = path.IndexOf("]", start);
			if (start >= 0 && end > start)
			{
				if (int.TryParse(path.Substring(start, end - start), out int result))
					return result;
			}
			return -1;
		}
	}
}
