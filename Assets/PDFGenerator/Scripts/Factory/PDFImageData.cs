using System;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PDFImageData : BasePDFData
	{
		public string url;
		public float width;
		public float height;
		public float x;
		public float y;
		public ImageStyleData style;

		public PDFImageData(PDFJObjectFactory factory) : base(factory)
		{
			type = PDFConstants.k_ImageType;
			width = 100;
			height = 100;
		}

		public PDFImageData SetUrl(string url)
		{
			this.url = url;
			return this;
		}

		public PDFImageData SetWidth(float width)
		{
			this.width = width;
			return this;
		}

		public PDFImageData SetHeight(float height)
		{
			this.height = height;
			return this;
		}

		public PDFImageData SetSize(float width, float height)
		{
			return SetWidth(width).SetHeight(height);
		}

		public PDFImageData SetPosition(float x, float y)
		{
			this.x = x;
			this.y = y;
			return this;
		}

		public PDFImageData SetStyle(ImageStyleData style)
		{
			this.style = style;
			return this;
		}

		public PDFImageData SetStyle(ImageDisplayType display, int margin, bool marginAuto, int width, int height)
		{
			return SetStyle(new(display, margin, marginAuto, width, height));
		}
	}
}
