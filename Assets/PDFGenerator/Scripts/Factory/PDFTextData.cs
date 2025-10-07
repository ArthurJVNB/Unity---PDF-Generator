using System;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PDFTextData : BasePDFData
	{
		public string text;
		public int fontSize;
		public string align;
		public TextStyleData style;

		public PDFTextData(PDFJObjectFactory factory) : base(factory)
		{
			type = PDFConstants.k_TextType;
			fontSize = 12;
			align = "left";
		}

		public PDFTextData SetText(string text)
		{
			this.text = text;
			return this;
		}

		public PDFTextData SetFontSize(int fontSize)
		{
			this.fontSize = fontSize;
			return this;
		}

		public PDFTextData SetAlign(string align)
		{
			this.align = align;
			return this;
		}

		public TextStyleData AddStyle(int fontSize)
		{
			style = new TextStyleData(this, fontSize);
			return style;
		}
	}
}
