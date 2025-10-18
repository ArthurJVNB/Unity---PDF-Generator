using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;

namespace Project.PDFGenerator.Tests
{
	public class PDFTextDataTest
	{
		[Test]
		public void TestJsonTitle()
		{
			const string expected = "{\"type\":\"title\",\"text\":\"Relatorio de Vendas - Outubro 2025\",\"style\":{\"font-size\":\"26px\",\"text-align\":\"center\",\"font-weight\":\"bold\",\"color\":\"#2E86C1\",\"margin-bottom\":\"25px\"}}";

			ColorUtility.TryParseHtmlString("#2E86C1", out Color color);
			var actual = new PDFTextData(null).SetType(TextType.Title).SetText("Relatorio de Vendas - Outubro 2025")
				.AddStyle(26).SetTextAlign(TextAlignType.Center).SetColor(color).SetMarginBottom(25).SetFontWeight(FontWeight.Bold).DoneStyle()
				.GetExportData().ToString(Formatting.None);

			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestJsonParagraph()
		{
			const string expected = "{\"type\":\"paragraph\",\"text\":\"Este eh um paragrafo.\"," +
				"\"style\":{\"font-size\":\"14px\",\"text-align\":\"justify\",\"line-height\":\"1.4\",\"margin-bottom\":\"15px\"}}";
			var actual = new PDFTextData(null).SetType(TextType.Paragraph).SetText("Este eh um paragrafo.")
				.AddStyle(14).SetTextAlign(TextAlignType.Justify).SetLineHeight(1.4f).SetMarginBottom(15).DoneStyle()
				.GetExportData().ToString(Formatting.None);

			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestJsonSubtitle()
		{
			const string expected = "{\"type\":\"subtitle\",\"text\":\"Resumo Geral\",\"style\":{\"font-size\":\"18px\",\"font-weight\":\"bold\",\"color\":\"#1B4F72\",\"margin-top\":\"25px\",\"margin-bottom\":\"10px\"}}";
			var actual = new PDFTextData(null).SetType(TextType.Subtitle).SetText("Resumo Geral")
				.AddStyle(18).SetColor(PDFColorUtility.ParseHtmlString("#1B4F72")).SetMarginTop(25).SetMarginBottom(10).SetFontWeight(FontWeight.Bold).DoneStyle()
				.GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestJsonFooter()
		{
			const string expected = "{\"type\":\"footer\",\"text\":\"Propixel Games c 2025 - Relatorios automatizados via Unity\",\"style\":{\"font-size\":\"10px\",\"text-align\":\"center\",\"color\":\"#888888\",\"margin-top\":\"40px\"}}";
			var actual = new PDFTextData(null).SetType(TextType.Footer).SetText("Propixel Games c 2025 - Relatorios automatizados via Unity")
				.AddStyle(10).SetTextAlign(TextAlignType.Center).SetColor(PDFColorUtility.ParseHtmlString("#888")).SetMarginTop(40).DoneStyle()
				.GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void TestJsonNoStyle()
		{
			const string expected = "{\"type\":\"paragraph\",\"text\":\"Text without style\",\"style\":{}}";
			var actual = new PDFTextData(null).SetType(TextType.Paragraph).SetText("Text without style").GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}
	}
}
