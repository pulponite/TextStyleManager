using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TextStyleManager
{
	/// <summary>
	/// Manages a collection of TextStyleType objects, bundling them together as a style sheet template.
	/// This object also tracks which style map is currently active for the sheet, and refreshes the styles
	/// on active switcher when that changes.
	/// </summary>
	[CreateAssetMenu(fileName = "TextStyleSet", menuName = "TextStyleManager/Text Style Set")]
	public class TextStyleSet : ScriptableObject
	{
		#pragma warning disable 0649
		[SerializeField]
		private List<TextStyleType> textTypes;
		#pragma warning restore 0649

		[SerializeField]
		private TextStyleMap startingStyleMap;

		private TextStyleMap currentStyleMap;

		/// <summary>
		/// The TextStyleMap currently in use. If one hasn't been set at runtime, it will default to the startingStyleMap.
		/// </summary>
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

		/// <summary>
		/// The collection of TextStyleTypes bundles in the set.
		/// </summary>
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

		/// <summary>
		/// Set the currently active TextStyleMap, and update all labels.
		/// </summary>
		public void SetActiveStyleMap(TextStyleMap styleMap)
		{
			currentStyleMap = styleMap;
			TextStyleSwitcher.RefreshStylesInScene();
		}

		/// <summary>
		/// Lookup the TextStyle registered for the given type, returning null if none exists.
		/// </summary>
		public TextStyle GetStyle(TextStyleType textType)
		{
			return ActiveStyleMap?.GetStyle(textType);
		}
	}
}