using Newtonsoft.Json;
using NUnit.Framework;

namespace Project.PDFGenerator.Tests
{
	public class PDFImageDataTest
	{
		[Test]
		public void TestJson()
		{
			const string expected = "{\"type\":\"image\",\"src\":\"https://www.propixelgames.online/pdf/images/icon.png\"," +
				"\"style\":{\"display\":\"block\",\"margin\":\"20px auto\",\"width\":\"120px\",\"height\":\"120px\",\"border\":\"2px solid #2E86C1\",\"border-radius\":\"10px\"," +
				"\"background-color\":\"#EAF2F8\",\"padding\":\"5px\"}}";
			var actual = new PDFImageData(null).SetUrl("https://www.propixelgames.online/pdf/images/icon.png")
				.AddStyle().SetDisplay(ImageDisplayType.Block).SetMargin(20, true).SetSize(120, 120).SetBorder(2, PDFColorUtility.ParseHtmlString("2E86C1")).SetBorderRadius(10)
				.SetBackgroundColor(PDFColorUtility.ParseHtmlString("EAF2F8")).SetPadding(5).DoneStyle()
				.GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestJsonStyle()
		{
			const string expected = "\"style\":{\"display\":\"block\",\"margin\":\"20px auto\",\"width\":\"120px\",\"border\":\"2px solid #2E86C1\"," +
				"\"border-radius\":\"10px\",\"background-color\":\"#EAF2F8\",\"padding\":\"5px\"}";
			var actual = new ImageStyleData().SetDisplay(ImageDisplayType.Block).SetMargin(20, true).SetWidth(120)
				.AddBorder().SetWidth(2).SetType(BorderType.Solid).SetColor(PDFColorUtility.ParseHtmlString("2E86C1")).DoneBorder()
				.SetBorderRadius(10).SetBackgroundColor(PDFColorUtility.ParseHtmlString("EAF2F8")).SetPadding(5)
				.GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}
	}
}
