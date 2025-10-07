using System;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public abstract class BasePDFData
	{
		public string type;
		public float x;
		public float y;
		internal PDFJObjectFactory factory;

		public BasePDFData(PDFJObjectFactory factory)
		{
			this.factory = factory;
			x = 50;
			y = 50;
		}

		public BasePDFData SetPosition(float x, float y)
		{
			this.x = x;
			this.y = y;
			return this;
		}

		public PDFJObjectFactory Done()
		{
			return factory;
		}
	}

	[Serializable]
	public class PDFTextData : BasePDFData
	{
		public string text;
		public int fontSize;
		public string align;

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
	}

	[Serializable]
	public class PDFImageData : BasePDFData
	{
		public string url;
		public float width;
		public float height;

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
	}
}
