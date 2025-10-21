using System;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PDFImageData : BasePDFData
	{
		public string url;
		public ImageStyleData style;

		public PDFImageData(PDFJObjectFactory factory) : base(factory)
		{
			type = PDFConstants.k_ImageType;
			style = new(this);
		}

		public PDFImageData SetId(string id)
		{
			this.id = id;
			return this;
		}

		public PDFImageData SetUrl(string url)
		{
			this.url = url;
			return this;
		}

		public ImageStyleData AddStyle()
		{
			style = new(this);
			return style;
		}

		public ImageStyleData AddStyle(int width , int height, ImageDisplayType display = ImageStyleData.k_DefaultDisplayType)
		{
			return AddStyle().SetSize(width, height).SetDisplay(display);
		}

		public ImageStyleData AddStyle(int width, int height, int margin, bool marginAuto = ImageStyleData.k_DefaultMarginAuto, ImageDisplayType display = ImageStyleData.k_DefaultDisplayType)
		{
			return AddStyle(width, height, display).SetMargin(margin, marginAuto);
		}

		public PDFImageData SetStyle(ImageStyleData style)
		{
			style._parent = this;
			this.style = style;
			return this;
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
