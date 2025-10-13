using NUnit.Framework;

namespace Project.PDFGenerator.Tests
{
	public class ListDataTest
	{
		[Test]
		public void ListDataTestSimplePasses()
		{
			const string expected = "{\"type\":\"list\",\"ordered\":false,\"items\":[\"Item 1\",\"Item 2\",\"Item 3\"]," +
				"\"style\":{\"font-size\":\"13px\",\"line-height\":\"1.6\",\"color\":\"#212F3C\",\"margin-left\":\"25px\",\"list-style-type\":\"disc\"}}";
			var actual = new PDFListData(null).SetOrdered(false).AddItem("Item 1").AddItem("Item 2").AddItem("Item 3")
				.AddStyle(13).SetColor(PDFColorUtility.ParseHtmlString("#212F3C")).SetMarginLeft(25).SetLineHeight(1.6f).SetListStyleType(ListStyleType.Disc).DoneStyle()
				.GetExportData().ToString(Newtonsoft.Json.Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		//TODO: test all styles of ListStyleData
	}
}
