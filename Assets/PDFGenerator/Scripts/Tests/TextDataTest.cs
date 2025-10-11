using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;

namespace Project.PDFGenerator.Tests
{
	public class TextDataTest
	{
		// A Test behaves as an ordinary method
		[Test]
		public void TitleDataTestJson()
		{
			// Use the Assert class to test conditions
			const string expected = "{\"type\":\"title\",\"text\":\"Relatório de Vendas - Outubro 2025\",\"style\":{\"font-size\":\"26px\",\"text-align\":\"center\",\"font-weight\":\"bold\",\"color\":\"#2E86C1\",\"margin-bottom\":\"25px\"}}";

			ColorUtility.TryParseHtmlString("#2E86C1", out Color color);
			var actual = new PDFTextData(null).SetType(TextType.Title).SetText("Relatório de Vendas - Outubro 2025")
				.AddStyle(26).SetTextAlign(TextAlignType.Center).SetColor(color).SetMarginBottom(25).SetFontWeight(FontWeight.Bold).DoneStyle()
				.GetExportData().ToString(Formatting.None);

			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void ParagraphDataTestJson()
		{
			const string expected = "{\"type\":\"paragraph\",\"text\":\"Este eh um paragrafo.\"," +
				"\"style\":{\"font-size\":\"14px\",\"text-align\":\"justify\",\"line-height\":\"1.4\",\"margin-bottom\":\"15px\"}}";
			var actual = new PDFTextData(null).SetType(TextType.Paragraph).SetText("Este eh um paragrafo.")
				.AddStyle(14).SetTextAlign(TextAlignType.Justify).SetLineHeight(1.4f).SetMarginBottom(15).DoneStyle()
				.GetExportData().ToString(Formatting.None);

			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void SubtitleDataTestJson()
		{
			const string expected = "{\"type\":\"subtitle\",\"text\":\"Resumo Geral\",\"style\":{\"font-size\":\"18px\",\"font-weight\":\"bold\",\"color\":\"#1B4F72\",\"margin-top\":\"25px\",\"margin-bottom\":\"10px\"}}";
			var actual = new PDFTextData(null).SetType(TextType.Subtitle).SetText("Resumo Geral")
				.AddStyle(18).SetColor(PDFColorUtility.ParseHtmlString("#1B4F72")).SetMarginTop(25).SetMarginBottom(10).SetFontWeight(FontWeight.Bold).DoneStyle()
				.GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}

		[Test]
		public void FooterDataTestJson()
		{
			const string expected = "{\"type\":\"footer\",\"text\":\"Propixel Games © 2025 — Relatórios automatizados via Unity\",\"style\":{\"font-size\":\"10px\",\"text-align\":\"center\",\"color\":\"#888888\",\"margin-top\":\"40px\"}}";
			var actual = new PDFTextData(null).SetType(TextType.Footer).SetText("Propixel Games © 2025 — Relatórios automatizados via Unity")
				.AddStyle(10).SetTextAlign(TextAlignType.Center).SetColor(PDFColorUtility.ParseHtmlString("#888")).SetMarginTop(40).DoneStyle()
				.GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}
	}
}
