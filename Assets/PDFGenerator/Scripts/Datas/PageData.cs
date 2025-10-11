using System;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PageData : IExportable<JObject>
	{
		public PageSize size = PageSize.A4;
		public PageOrientation orientation = PageOrientation.Portrait;
		public PageMargins margins = new();

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

		public PageData SetMargins(int top, int right, int bottom, int left)
		{
			this.margins.top = top;
			this.margins.right = right;
			this.margins.bottom = bottom;
			this.margins.left = left;
			return this;
		}

		public PageData SetMargins(int all)
		{
			return SetMargins(all, all, all, all);
		}

		public PageData SetMargins(int vertical, int horizontal)
		{
			return SetMargins(vertical, horizontal, vertical, horizontal);
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
