using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextStyleManager
{

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

		public void ApplyToTMPText(TMPro.TMP_Text textComponent)
		{
			textComponent.font = fontAsset;
			textComponent.fontMaterial = fontMaterial;
			textComponent.isRightToLeftText = textDirection == TextDirection.RightToLeft;
		}
	}

}