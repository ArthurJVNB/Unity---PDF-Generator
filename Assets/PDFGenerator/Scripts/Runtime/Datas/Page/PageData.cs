using System;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PageData : IExportable<JObject>
	{
		public const PageSize k_DefaultSize = PageSize.A4;
		public const PageOrientation k_DefaultOrientation = PageOrientation.Portrait;
		public const int k_DefaultMargin = 40;

		public PageSize size = k_DefaultSize;
		public PageOrientation orientation = k_DefaultOrientation;
		public PageMargins margins = new() { top = k_DefaultMargin, bottom = k_DefaultMargin, left = k_DefaultMargin, right = k_DefaultMargin };

		internal PDFJObjectFactory _parent;

		public PageData() { }

		public PageData SetSize(PageSize size)
		{
			this.size = size;
			return this;
		}


		public PageData SetOrientation(PageOrientation orientation)
		{
			this.orientation = orientation;
			return this;
		}

		public PageData SetMargins(int top, int bottom, int left, int right)
		{
			this.margins.top = top;
			this.margins.bottom = bottom;
			this.margins.left = left;
			this.margins.right = right;
			return this;
		}

		public PageData SetMargins(int all)
		{
			return SetMargins(all, all, all, all);
		}

		public PageData SetMargins(int vertical, int horizontal)
		{
			return SetMargins(vertical, vertical, horizontal, horizontal);
		}

		public PDFJObjectFactory Done()
		{
			return _parent;
		}

		public JObject GetExportData()
		{
			return new JObject(
					new JProperty("size", this.size.ToString()),
					new JProperty("orientation", this.orientation.ToString().ToLower()),
					new JProperty("margins", this.margins.GetExportData())
			);

			//return new JObject(
			//	new JProperty("page", new JObject(
			//		new JProperty("size", this.size.ToString()),
			//		new JProperty("orientation", this.orientation.ToString().ToLower()),
			//		new JProperty("margins", this.margins.GetExportData())
			//	))
			//);
		}
	}
}
