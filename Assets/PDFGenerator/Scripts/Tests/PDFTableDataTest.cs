using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;

namespace Project.PDFGenerator.Tests
{
	public class PDFTableDataTest
	{
		[Test]
		public void TestJson()
		{
			const string expected = "{\"type\":\"table\",\"header\":[\"Produto\",\"Quantidade\",\"Preco\",\"Total\"],\"rows\":[[\"Notebook\",\"2\",\"R$ 3.500\",\"R$ 7.000\"],[\"Mouse Gamer\",\"5\",\"R$ 150\",\"R$ 750\"]," +
				"[\"Teclado Mecanico\",\"3\",\"R$ 400\",\"R$ 1.200\"]],\"style\":{\"width\":\"100%\",\"border\":\"1px solid #000000\",\"font-size\":\"13px\",\"text-align\":\"center\"," +
				"\"margin-bottom\":\"20px\"},\"headerStyle\":{\"background-color\":\"#2E86C1\",\"color\":\"#FFFFFF\",\"font-weight\":\"bold\",\"padding\":\"6px\"},\"cellStyle\":{\"border\":\"1px solid #000000\",\"padding\":\"5px\"}," +
				"\"rowStyle\":[{\"background-color\":\"#ECF0F1\"},{\"background-color\":\"#FFFFFF\"}]}";
			var actual = new PDFTableData(null).SetHeader("Produto", "Quantidade", "Preco", "Total")
				.AddRow("Notebook", "2", "R$ 3.500", "R$ 7.000").AddRow("Mouse Gamer", "5", "R$ 150", "R$ 750").AddRow("Teclado Mecanico", "3", "R$ 400", "R$ 1.200")
				.AddStyle().SetWidthPercent(100).AddBorder().SetWidth(1).SetType(BorderType.Solid).SetColor(PDFColorUtility.ParseHtmlString("000")).DoneBorder()
				.SetFontSize(13).SetTextAlign(TextAlignType.Center).SetMarginBottom(20).DoneStyle()
				.AddHeaderStyle().SetBackgroundColor(PDFColorUtility.ParseHtmlString("#2E86C1")).SetColor(PDFColorUtility.ParseHtmlString("FFFFFF")).SetFontWeight(FontWeight.Bold).SetPadding(6).DoneStyle()
				.AddCellStyle().SetBorder(1, PDFColorUtility.ParseHtmlString("#000")).SetPadding(5).DoneStyle()
				.AddRowStyle().AddBackgroundColor(PDFColorUtility.ParseHtmlString("ECF0F1")).AddBackgroundColor(PDFColorUtility.ParseHtmlString("FFFFFF")).DoneStyle()
				.GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestJsonStyle()
		{
			const string expected = "\"style\":{\"width\":\"100%\",\"border\":\"1px solid #000000\",\"font-size\":\"13px\",\"text-align\":\"center\",\"margin-bottom\":\"20px\"}";
			var actual = new TableStyleData(null).SetWidthPercent(100).SetBorder(1, PDFColorUtility.ParseHtmlString("000"), BorderType.Solid).SetFontSize(13).SetTextAlign(TextAlignType.Center).SetMarginBottom(20)
				.GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestJsonHeaderStyle()
		{
			const string expected = "\"headerStyle\":{\"background-color\":\"#2E86C1\",\"color\":\"#FFFFFF\",\"font-weight\":\"bold\",\"padding\":\"6px\"}";
			var actual = new TableHeaderStyleData(null).SetBackgroundColor(PDFColorUtility.ParseHtmlString("2E86C1")).SetColor(PDFColorUtility.ParseHtmlString("#FFFFFF")).SetFontWeight(FontWeight.Bold).SetPadding(6)
				.GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestJsonCellStyle()
		{
			const string expected = "\"cellStyle\":{\"border\":\"1px solid #000000\",\"padding\":\"5px\"}";
			var actual = new TableCellStyleData(null).SetBorder(1, Color.black).SetPadding(5).GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestJsonRowStyle()
		{
			const string expected = "\"rowStyle\":[{\"background-color\":\"#ECF0F1\"},{\"background-color\":\"#FFFFFF\"}]";
			var actual = new TableRowStyleData(null).AddBackgroundColor(PDFColorUtility.ParseHtmlString("ECF0F1")).AddBackgroundColor(PDFColorUtility.ParseHtmlString("#FFFFFF")).GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestId()
		{
			const string Id = "table_01";
			var factory = PDFJObjectFactory.Start().Done().AddTable().SetHeader("Header 1", "Header 2").SetId(Id).Done();
			Assert.IsTrue(factory.TryGetById(Id, out PDFTableData _), $"Data with ID '{Id}' not found.");
		}

		[Test]
		public void TestIdUpdateData()
		{
			const string Id = "text_01";
			var factory = PDFJObjectFactory.Start().Done().AddTable().SetHeader("Old Header 1", "Old Header 2").AddRow("Value 1", "Value 2").SetId(Id).Done();
			var data = factory[Id] as PDFTableData;
			Assert.IsNotNull(data, $"Data with ID '{Id}' not found.");

			string[] newHeader = new string[] { "New Header 1", "New Header 2" };
			data.SetHeader(newHeader);
			Assert.AreEqual(newHeader.Length, data.header.Count, "Table header content was not updated correctly.");
			for (int i = 0; i < data.header.Count; i++)
				Assert.AreEqual(newHeader[i], data.header[i], "Table header content was not updated correctly.");

			string[] newRow = new string[] { "New Value 1", "New Value 2" };
			data.AddRow(newRow);
			Assert.AreEqual(2, data.rows.Count, "Table row count was not updated correctly.");
			for (int i = 2; i < data.rows.Count; i++)
				for (int j = 0; j < data.rows[i].Count; j++)
					Assert.AreEqual(newRow[j], data.rows[i][j], "Table row content was not updated correctly.");
		}
	}
}
