using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TextStyleManager
{

	[System.Serializable]
	public struct TextStyleMapEntry
	{
		public TextStyleType textType;
		public TextStyle style;
	}

	[CreateAssetMenu(fileName = "TextStyleMap", menuName = "FontStyleManager/Text Style Map")]
	public class TextStyleMap : ScriptableObject
	{
		[SerializeField]
		private TextStyle fallbackStyle;

		[SerializeField]
		private TextStyleSet styleSet;

		[SerializeField]
		private List<TextStyleMapEntry> entries;

		public List<TextStyleType> registeredTypes
		{
			get
			{
				var result = new List<TextStyleType>();
				foreach (var entry in entries)
				{
					result.Add(entry.textType);
				}
				return result;
			}
		}

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

		public TextStyle GetStyle(TextStyleType textType)
		{
			TextStyle result = null;
			if (!Mapping.TryGetValue(textType, out result))
			{
				result = fallbackStyle;
			}
			return result;
		}

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

		public void MarkMappingDirty()
		{
			mapping = null;
		}

		private void RefreshMapping()
		{
			mapping = new Dictionary<TextStyleType, TextStyle>();
			for (int i = 0; i < entries.Count; i++)
			{
				var entry = entries[i];
				mapping[entry.textType] = entry.style;
			}
		}
	}

}