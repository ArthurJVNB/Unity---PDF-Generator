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

		public PDFTextData SetId(string id)
		{
			this.id = id;
			return this;
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

		public TextStyleData AddStyle()
		{
			style = new TextStyleData(this);
			return style;
		}

		public TextStyleData AddStyle(int fontSize)
		{
			return (TextStyleData)AddStyle().SetFontSize(fontSize);
		}

		public PDFTextData SetStyle(TextStyleData style)
		{
			style._parent = this;
			this.style = style;
			return this;
		}

		public override JObject GetExportData()
		{
			//style ??= new(this);
			return new JObject()
			{
				new JProperty("type", type),
				new JProperty("text", text),
				new JProperty("style", style?.GetExportData() ?? new JObject()),
			};
		}
	}
}
