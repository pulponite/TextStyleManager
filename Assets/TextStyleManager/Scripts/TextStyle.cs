using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextStyleManager
{
	/// <summary>
	/// A TextStyle represents a specific font material pair that can be applied to a label.
	/// </summary>
	[CreateAssetMenu(fileName = "TextStyle", menuName = "TextStyleManager/Text Style")]
	public class TextStyle : ScriptableObject
	{
		public enum TextDirection
		{
			LeftToRight,
			RightToLeft
		}

		public TMPro.TMP_FontAsset fontAsset;
		public Material fontMaterial;
		public TextDirection textDirection;

		/// <summary>
		/// Given a TMP_Text component, apply the settings from this style to it directly.
		/// </summary>
		/// <param name="textComponent"></param>
		public void ApplyToTMPText(TMPro.TMP_Text textComponent)
		{
			textComponent.font = fontAsset;
			textComponent.fontMaterial = fontMaterial;
			textComponent.isRightToLeftText = textDirection == TextDirection.RightToLeft;
		}
	}

}