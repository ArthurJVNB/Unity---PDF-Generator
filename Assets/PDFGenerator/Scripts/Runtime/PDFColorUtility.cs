using UnityEngine;

namespace Project.PDFGenerator
{
	public static class PDFColorUtility
	{
		public const string k_PrefixColorHtml = "#";

		public static Color ParseHtmlString(string hex)
		{
			ColorUtility.TryParseHtmlString(hex, out var color);
			return color;
		}

		public static string ToHtmlStringRGBA(Color color)
		{
			return k_PrefixColorHtml + ColorUtility.ToHtmlStringRGBA(color);
		}

		public static string ToHtmlStringRGB(Color color)
		{
			return k_PrefixColorHtml + ColorUtility.ToHtmlStringRGB(color);
		}
	}
}
