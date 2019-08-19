using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace TextStyleManager
{

	[CustomEditor(typeof(TextStyleSwitcher))]
	public class TextStyleSwitcherEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			List<TextStyleType> styleTypes = new List<TextStyleType>();
			List<string> styleTypeNames = new List<string>();

			var styleSetProperty = serializedObject.FindProperty("styleSet");
			var textTypeProperty = serializedObject.FindProperty("textType");

			GUILayout.BeginVertical();
			{
				EditorGUILayout.Space();
				EditorGUILayout.ObjectField(styleSetProperty);

				if (styleSetProperty.objectReferenceValue != null)
				{
					int selectedIndex = -1;

					var styleSet = styleSetProperty.objectReferenceValue as TextStyleSet;
					foreach (var textType in styleSet.TextTypes)
					{
						if (textType == textTypeProperty.objectReferenceValue)
						{
							selectedIndex = styleTypes.Count;
						}

						styleTypes.Add(textType);
						styleTypeNames.Add(textType.name);
					}

					selectedIndex = EditorGUILayout.Popup("Text Type", selectedIndex, styleTypeNames.ToArray());
					if (selectedIndex >= 0)
					{
						textTypeProperty.objectReferenceValue = styleTypes[selectedIndex];
					}
				}
				else
				{
					textTypeProperty.objectReferenceValue = null;
				}
			}
			GUILayout.EndVertical();

			serializedObject.ApplyModifiedProperties();
		}
	}

}