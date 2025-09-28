using System;
using UnityEditor;
using UnityEngine;

namespace Core.Inspector
{
	[CustomPropertyDrawer(typeof(EnumArrayAttribute))]
	public class EnumArrayDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EnumArrayAttribute enumAttr = (EnumArrayAttribute)attribute;
			string[] enumNames = Enum.GetNames(enumAttr.EnumType);

			// Przygotuj etykietę jaka ma być widoczna (nie zmieniamy oryginalnego 'label' bezpośrednio)
			GUIContent displayedLabel = new GUIContent(label);

			// Jeśli to element tablicy (np. Array.data[x]) -> podstaw nazwę enuma
			int index = TryGetArrayIndex(property.propertyPath);
			if (index >= 0 && index < enumNames.Length)
			{
				displayedLabel.text = enumNames[index];
			}

			EditorGUI.BeginProperty(position, displayedLabel, property);
			// true -> rysujemy również dzieci (rozszerzalne pola struktury)
			EditorGUI.PropertyField(position, property, displayedLabel, true);
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			// Musimy użyć tej samej etykiety jak w OnGUI, żeby wysokość się zgadzała
			EnumArrayAttribute enumAttr = (EnumArrayAttribute)attribute;
			string[] enumNames = Enum.GetNames(enumAttr.EnumType);

			GUIContent displayedLabel = new GUIContent(label);
			int index = TryGetArrayIndex(property.propertyPath);
			if (index >= 0 && index < enumNames.Length)
			{
				displayedLabel.text = enumNames[index];
			}

			// EditorGUI.GetPropertyHeight NIE jest naszą metodą override — to statyczne API Unity,
			// które zwraca prawidłową wysokość łącznie z dziećmi gdy trzeci parametr = true.
			return EditorGUI.GetPropertyHeight(property, displayedLabel, true);
		}

		private int TryGetArrayIndex(string propertyPath)
		{
			// Szukamy wzorca "Array.data[<index>]" i wyciągamy <index>
			const string marker = "Array.data[";
			int m = propertyPath.IndexOf(marker, StringComparison.Ordinal);
			if (m < 0) return -1;
			int start = m + marker.Length;
			int end = propertyPath.IndexOf(']', start);
			if (end < 0) return -1;

			if (int.TryParse(propertyPath.Substring(start, end - start), out int result))
				return result;
			return -1;
		}
	}
}
