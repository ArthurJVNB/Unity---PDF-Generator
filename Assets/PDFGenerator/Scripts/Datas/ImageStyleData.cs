using System;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	[Serializable]
	public class ImageStyleData : IExportable<JObject>
	{
		private const string px = PDFConstants.k_Pixel;

		public ImageDisplayType display = ImageDisplayType.Block;
		public int margin = 10;
		public bool marginAuto = true;
		public int width = 120;
		public int height = 120;

		internal PDFImageData _parent;

		public ImageStyleData() { }

		public ImageStyleData(PDFImageData parent)
		{
			_parent = parent;
		}

		public ImageStyleData(PDFImageData parent, ImageDisplayType display, int margin, bool marginAuto, int width, int height) : this(parent)
		{
			this.display = display;
			this.margin = margin;
			this.marginAuto = marginAuto;
			this.width = width;
			this.height = height;
		}

		public ImageStyleData SetDisplay(ImageDisplayType display)
		{
			this.display = display;
			return this;
		}

		public ImageStyleData SetMargin(int margin, bool marginAuto = true)
		{
			this.margin = margin;
			this.marginAuto = marginAuto;
			return this;
		}

		public ImageStyleData SetWidth(int width)
		{
			this.width = width;
			return this;
		}

		public ImageStyleData SetHeight(int height)
		{
			this.height = height;
			return this;
		}

		public ImageStyleData SetSize(int width, int height)
		{
			return SetWidth(width).SetHeight(height);
		}

		public PDFImageData DoneStyle()
		{
			return _parent;
		}

		public JObject GetExportData()
		{
			return new JObject()
			{
				{ "style", new JObject
					{
						{ "display", display.ToString().ToLower() },
						{ "margin", $"{margin}{px}{(marginAuto ? " auto" : string.Empty)}" },
						{ "width", $"{width}{px}" },
						{ "height", $"{height}{px}" }
					}
				}
			};
		}
	}
}
