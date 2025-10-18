using System.Globalization;

namespace Project.PDFGenerator
{
	public static class PDFExtensions
	{
		public static string ToStringPDF(this float value)
		{
			return value.ToString(new CultureInfo("en-US"));
		}

		public static string ToStringPDF(this ImageDisplayType display)
		{
			if (display == ImageDisplayType.InlineBlock)
				return "inline-block";
			return display.ToString().ToLower();
		}
	}
}
