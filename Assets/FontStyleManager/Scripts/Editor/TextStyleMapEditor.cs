using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace TextStyleManager
{

	[CustomEditor(typeof(TextStyleMap))]
	public class TextStyleMapEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			var styleSetProperty = serializedObject.FindProperty("styleSet");
			var previousStyleSet = styleSetProperty.objectReferenceValue;

			EditorGUILayout.PropertyField(styleSetProperty);
			EditorGUILayout.Space();

			if (styleSetProperty.objectReferenceValue != null)
			{
				serializedObject.ApplyModifiedProperties();
				var tsm = target as TextStyleMap;
				if (tsm.ValidateStyleEntries())
				{
					serializedObject.Update();
				}
			}

			if (styleSetProperty.objectReferenceValue != null)
			{
				GUILayout.BeginVertical("box");
				EditorGUILayout.LabelField("Style Map", EditorStyles.boldLabel);
				EditorGUILayout.Space();
				EditorGUI.indentLevel += 1;

				var entriesProperty = serializedObject.FindProperty("entries");

				Dictionary<TextStyleType, (int, SerializedProperty)> textTypeIndices = new Dictionary<TextStyleType, (int, SerializedProperty)>();
				for (int i = 0; i < entriesProperty.arraySize; i++)
				{
					var entry = entriesProperty.GetArrayElementAtIndex(i);
					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.ObjectField(entry.FindPropertyRelative("style"), new GUIContent(entry.FindPropertyRelative("textType").objectReferenceValue.name));
					}
					GUILayout.EndHorizontal();
				}
				EditorGUI.indentLevel -= 1;
				GUILayout.EndVertical();
			}

			if (serializedObject.ApplyModifiedProperties())
			{
				(target as TextStyleMap).MarkMappingDirty();
				TextStyleSwitcher.RefreshStylesInScene();
			}
		}
	}

}