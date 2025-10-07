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

		public ImageStyleData() { }

		public ImageStyleData(ImageDisplayType display, int margin, bool marginAuto, int width, int height)
		{
			this.display = display;
			this.margin = margin;
			this.marginAuto = marginAuto;
			this.width = width;
			this.height = height;
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
