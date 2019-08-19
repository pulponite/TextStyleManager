using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TextStyleManager
{
	/// <summary>
	/// Stores a mapping from TextStyleTypes to TextStyle for a given TextStyleSheet.
	/// </summary>
	[CreateAssetMenu(fileName = "TextStyleMap", menuName = "TextStyleManager/Text Style Map")]
	public class TextStyleMap : ScriptableObject
	{
		#pragma warning disable 0649
		[SerializeField]
		private TextStyle fallbackStyle;

		[SerializeField]
		private TextStyleSet styleSet;

		[SerializeField]
		private List<TextStyleMapEntry> entries;
		#pragma warning restore 0649

		private Dictionary<TextStyleType, TextStyle> mapping = null;
		private Dictionary<TextStyleType, TextStyle> Mapping
		{
			get
			{
				if (mapping == null) RefreshMapping();
				return mapping;
			}
		}

		public void Awake()
		{
			RefreshMapping();
		}

		/// <summary>
		/// Lookup the style mapped to a given type.
		/// </summary>
		public TextStyle GetStyle(TextStyleType textType)
		{
			TextStyle result = null;
			if (!Mapping.TryGetValue(textType, out result))
			{
				result = fallbackStyle;
			}
			return result;
		}

		/// <summary>
		/// Ensure that the map has entries for all the TextStyleTypes in the associated TypeStyleSet,
		/// and remove entries for ones that aren't in the set. Mostly used by editor tooling.
		/// </summary>
		/// <returns>True if the entry set needed changing. False otherwise</returns>
		public bool ValidateStyleEntries()
		{
			bool appliedChange = false;

			if (entries == null)
			{
				appliedChange = true;
				entries = new List<TextStyleMapEntry>();
			}

			HashSet<TextStyleType> styleSetTypes = new HashSet<TextStyleType>(styleSet.TextTypes);

			for (int i = entries.Count - 1; i >= 0; i--)
			{
				if (styleSetTypes.Contains(entries[i].textType))
				{
					styleSetTypes.Remove(entries[i].textType);
				}
				else
				{
					appliedChange = true;
					entries.RemoveAt(i);
				}
			}

			foreach (var styleType in styleSetTypes)
			{
				appliedChange = true;
				var entry = new TextStyleMapEntry();
				entry.textType = styleType;
				entries.Add(entry);
			}

			return appliedChange;
		}

		/// <summary>
		/// Force the map between type and style to be rebuilt on the next query. Used by editor tooling.
		/// </summary>
		public void MarkMappingDirty()
		{
			mapping = null;
		}
		
		private void RefreshMapping()
		{
			if (entries == null) return;
			mapping = new Dictionary<TextStyleType, TextStyle>();
			for (int i = 0; i < entries.Count; i++)
			{
				var entry = entries[i];
				mapping[entry.textType] = entry.style;
			}
		}
	}

	[System.Serializable]
	public struct TextStyleMapEntry
	{
		public TextStyleType textType;
		public TextStyle style;
	}

}