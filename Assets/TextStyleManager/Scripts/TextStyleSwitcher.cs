using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TextStyleManager
{

	[RequireComponent(typeof(TMPro.TMP_Text))]
	public class TextStyleSwitcher : MonoBehaviour
	{
		#pragma warning disable 0649
		[SerializeField]
		private TextStyleSet styleSet;

		[SerializeField, HideInInspector]
		private TextStyleType textType;
		#pragma warning restore 0649

		private TMPro.TMP_Text TMPText
		{
			get
			{
				return GetComponent<TMPro.TMP_Text>();
			}
		}

		private void OnValidate()
		{
			RefreshStyle();
		}

		public void RefreshStyle()
		{
			if (styleSet == null || textType == null) return;

			styleSet.GetStyle(textType)?.ApplyToTMPText(TMPText);
		}

		public static void RefreshStylesInScene()
		{
			foreach (var switcher in FindObjectsOfType<TextStyleSwitcher>())
			{
				switcher.RefreshStyle();
			}
		}
	}

}