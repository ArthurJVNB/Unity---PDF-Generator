using System.Globalization;

namespace Project.PDFGenerator
{
	public static class PDFExtensions
	{
		public static string ToStringPDF(this float value)
		{
			return value.ToString(new CultureInfo("en-US"));
		}
	}
}
