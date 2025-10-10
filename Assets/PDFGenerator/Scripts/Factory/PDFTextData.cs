using System;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PDFTextData : BasePDFData
	{
		public string text;
		public TextStyleData style;

		public PDFTextData(PDFJObjectFactory factory) : base(factory)
		{
			type = PDFConstants.k_TextType;
		}

		public PDFTextData SetText(string text)
		{
			this.text = text;
			return this;
		}

		public PDFTextData SetType(TextType type)
		{
			this.type = type.ToString().ToLower();
			return this;
		}

		public TextStyleData AddStyle(int fontSize = PDFConstants.k_DefaultFontSize)
		{
			style = new TextStyleData(this, fontSize);
			return style;
		}

		public override JObject GetExportData()
		{
			return new JObject()
			{
				new JProperty("type", type),
				new JProperty("text", text),
				new JProperty("style", style.GetExportData()),
			};
		}
	}
}
