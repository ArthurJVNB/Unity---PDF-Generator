using Newtonsoft.Json;
using NUnit.Framework;

namespace Project.PDFGenerator.Tests
{
	public class PDFListDataTest
	{
		[Test]
		public void TestJson()
		{
			const string expected = "{\"type\":\"list\",\"ordered\":false,\"items\":[\"Item 1\",\"Item 2\",\"Item 3\"]," +
				"\"style\":{\"font-size\":\"13px\",\"line-height\":\"1.6\",\"color\":\"#212F3C\",\"margin-left\":\"25px\",\"list-style-type\":\"disc\"}}";
			var actual = new PDFListData(null).SetOrdered(false).AddItem("Item 1").AddItem("Item 2").AddItem("Item 3")
				.AddStyle(13).SetColor(PDFColorUtility.ParseHtmlString("#212F3C")).SetMarginLeft(25).SetLineHeight(1.6f).SetListStyleType(ListStyleType.Disc).DoneStyle()
				.GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestJsonAllStyles()
		{
			const string expected = "{\"type\":\"list\",\"ordered\":true,\"items\":[\"Item 1\",\"Item 2\",\"Item 3\"]," +
				"\"style\":{\"font-size\":\"13px\",\"font-style\":\"italic\",\"text-align\":\"left\",\"font-weight\":\"bold\",\"line-height\":\"1.5\"," +
				"\"color\":\"#283747\",\"margin-top\":\"30px\",\"margin-left\":\"30px\",\"margin-right\":\"30px\",\"margin-bottom\":\"30px\",\"list-style-type\":\"disc\"}}";
			var actual = new PDFListData(null).SetOrdered(true).AddItem("Item 1").AddItem("Item 2").AddItem("Item 3")
				.AddStyle(13).SetFontStyle(TextFontStyleType.Italic).SetTextAlign(TextAlignType.Left).SetFontWeight(FontWeight.Bold).SetLineHeight(1.5f)
				.SetColor(PDFColorUtility.ParseHtmlString("283747")).SetMarginTop(30).SetMarginLeft(30).SetMarginRight(30).SetMarginBottom(30).SetListStyleType(ListStyleType.Disc).DoneStyle()
				.GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestId()
		{
			const string Id = "list_01";
			var factory = PDFJObjectFactory.Start().Done().AddList().SetItems("Item 01", "Item 02").SetId(Id).Done();
			Assert.IsTrue(factory.TryGetById(Id, out PDFListData _), $"Data with ID '{Id}' not found.");
		}

		[Test]
		public void TestIdUpdateData()
		{
			const string Id = "image_01";
			var factory = PDFJObjectFactory.Start(PageSize.A4, PageOrientation.Portrait).AddList().SetItems("Wrong Item 01", "Wrong Item 02").SetId(Id).Done();
			var data = factory[Id] as PDFListData;
			Assert.IsNotNull(data, $"Data with ID '{Id}' not found.");

			string[] NewValues = new string[] { "Correct Item 01", "Correct Item 02" };
			data.SetItems(NewValues);
			Assert.AreEqual(NewValues.Length, data.items.Count, "List content was not updated correctly.");
			for (int i = 0; i < data.items.Count; i++)
				Assert.AreEqual(NewValues[i], data.items[i], "List content was not updated correctly.");
		}
	}
}
