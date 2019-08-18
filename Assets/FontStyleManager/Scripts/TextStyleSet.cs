using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TextStyleManager
{

	[SerializeField]
	public class TextStyleSetRegisteredMap
	{
		public string styleMapId;
		public TextStyleMap styleMap;
	}

	[CreateAssetMenu(fileName = "TextStyleSet", menuName = "FontStyleManager/Text Style Set")]
	public class TextStyleSet : ScriptableObject
	{
		[SerializeField]
		private List<TextStyleType> textTypes;

		[SerializeField]
		private List<TextStyleSetRegisteredMap> registeredStyleMaps;

		[SerializeField]
		private TextStyleMap startingStyleMap;

		private TextStyleMap currentStyleMap;

		public TextStyleMap ActiveStyleMap
		{
			get
			{
#if UNITY_EDITOR
				if (!EditorApplication.isPlaying)
				{
					currentStyleMap = null;
				}
#endif
				if (currentStyleMap == null)
				{
					return startingStyleMap;
				}
				return currentStyleMap;
			}
		}

		public ICollection<TextStyleType> TextTypes
		{
			get
			{
				return textTypes;
			}
		}

		public void Start()
		{
			startingStyleMap = currentStyleMap;
		}

		public void SetActiveStyleMap(TextStyleMap styleMap)
		{
			currentStyleMap = styleMap;
			TextStyleSwitcher.RefreshStylesInScene();
		}

		public TextStyle GetStyle(TextStyleType textType)
		{
			return ActiveStyleMap?.GetStyle(textType);
		}
	}

}