using System;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PDFImageData : BasePDFData
	{
		public string url;
		//public float width = 100;
		//public float height = 100;
		//public float x;
		//public float y;
		public ImageStyleData style;

		public PDFImageData(PDFJObjectFactory factory) : base(factory)
		{
			type = PDFConstants.k_ImageType;
			style = new(this);
		}

		public PDFImageData SetUrl(string url)
		{
			this.url = url;
			return this;
		}

		//public PDFImageData SetWidth(float width)
		//{
		//	this.width = width;
		//	return this;
		//}

		//public PDFImageData SetHeight(float height)
		//{
		//	this.height = height;
		//	return this;
		//}

		//public PDFImageData SetSize(float width, float height)
		//{
		//	return SetWidth(width).SetHeight(height);
		//}

		//public PDFImageData SetPosition(float x, float y)
		//{
		//	this.x = x;
		//	this.y = y;
		//	return this;
		//}

		public ImageStyleData AddStyle()
		{
			style = new(this);
			return style;
		}

		public PDFImageData AddStyle(ImageStyleData style)
		{
			style._parent = this;
			this.style = style;
			return this;
		}

		public ImageStyleData AddStyle(int width , int height, ImageDisplayType display = ImageStyleData.k_DefaultDisplayType)
		{
			return AddStyle().SetSize(width, height).SetDisplay(display);
		}

		public ImageStyleData AddStyle(int width, int height, int margin, bool marginAuto = ImageStyleData.k_DefaultMarginAuto, ImageDisplayType display = ImageStyleData.k_DefaultDisplayType)
		{
			return AddStyle(width, height, display).SetMargin(margin, marginAuto);
		}

		public override JObject GetExportData()
		{
			return new JObject()
			{
				new JProperty("type", type),
				new JProperty("src", url),
				style.GetExportData(),
			};
		}
	}
}
