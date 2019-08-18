using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


namespace TextStyleManager
{

	[CustomEditor(typeof(TextStyleSet))]
	public class TextStyleSetEditor : Editor
	{
		private ReorderableList textTypeList;

		private bool requireReimport = false;

		public void OnEnable()
		{
			SetupTextTypesList();
		}

		private void SetupTextTypesList()
		{
			textTypeList = new ReorderableList(serializedObject, serializedObject.FindProperty("textTypes"), true, true, true, true);

			textTypeList.drawHeaderCallback = (Rect rect) =>
			{
				EditorGUI.LabelField(rect, "Text Types");
			};

			textTypeList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
			{
				var element = textTypeList.serializedProperty.GetArrayElementAtIndex(index);
				if (element.objectReferenceValue == null) return;

				string newName = EditorGUI.DelayedTextField(new Rect(rect.x, rect.y + 2, rect.width, EditorGUIUtility.singleLineHeight), element.objectReferenceValue.name);
				if (!newName.Equals(element.objectReferenceValue.name))
				{
					element.objectReferenceValue.name = newName;
					requireReimport = true;
				}
			};

			textTypeList.onAddCallback += HandleAddTextType;
			textTypeList.onRemoveCallback += HandleRemoveTextType;
		}

		private void HandleAddTextType(ReorderableList list)
		{
			var index = list.serializedProperty.arraySize;
			list.serializedProperty.InsertArrayElementAtIndex(index);
			var element = list.serializedProperty.GetArrayElementAtIndex(index);

			TextStyleType textType = new TextStyleType();
			textType.name = "New Text Type";
			AssetDatabase.AddObjectToAsset(textType, target);

			element.objectReferenceValue = textType;
			requireReimport = true;
		}

		private void HandleRemoveTextType(ReorderableList list)
		{
			var element = list.serializedProperty.GetArrayElementAtIndex(list.index);
			AssetDatabase.RemoveObjectFromAsset(element.objectReferenceValue);
			element.objectReferenceValue = null;
			list.serializedProperty.DeleteArrayElementAtIndex(list.index);
			requireReimport = true;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.Space();
			textTypeList.DoLayoutList();

			EditorGUILayout.Space();
			var defaultStyleMapProperty = serializedObject.FindProperty("startingStyleMap");
			EditorGUILayout.PropertyField(defaultStyleMapProperty);


			EditorGUILayout.Space();
			//registeredMapsList.DoLayoutList();
			if (serializedObject.ApplyModifiedProperties())
			{
				TextStyleSwitcher.RefreshStylesInScene();
			}

			if (GUILayout.Button("Refresh Scene"))
			{
				TextStyleSwitcher.RefreshStylesInScene();
			}

			if (requireReimport)
			{
				AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(target));
				requireReimport = false;
			}
		}
	}

}