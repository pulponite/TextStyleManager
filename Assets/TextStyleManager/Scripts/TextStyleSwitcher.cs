using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TextStyleManager
{
	/// <summary>
	/// Is responsible for switching styles on TMPro labels, given the stylesheet and style type specified.
	/// </summary>
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

		/// <summary>
		/// Force a re-query and application of the applied style, if one exists.
		/// </summary>
		public void RefreshStyle()
		{
			if (styleSet == null || textType == null) return;

			styleSet.GetStyle(textType)?.ApplyToTMPText(TMPText);
		}

		/// <summary>
		/// Invoke a refresh style on all TextStyleSwitcher components that are active.
		/// </summary>
		public static void RefreshStylesInScene()
		{
			foreach (var switcher in FindObjectsOfType<TextStyleSwitcher>())
			{
				switcher.RefreshStyle();
			}
		}
	}

}