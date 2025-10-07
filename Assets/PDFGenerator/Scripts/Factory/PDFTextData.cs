using System;

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

		public TextStyleData AddStyle(int fontSize)
		{
			style = new TextStyleData(this, fontSize);
			return style;
		}
	}
}
