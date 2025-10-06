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

		public JObject GetExportData()
		{
			return new JObject(
					new JProperty("size", this.size.ToString()),
					new JProperty("orientation", this.orientation.ToString().ToLower()),
					new JProperty("margins", this.margins.GetExportData())
			);

			return new JObject(
				new JProperty("page", new JObject(
					new JProperty("size", this.size.ToString()),
					new JProperty("orientation", this.orientation.ToString().ToLower()),
					new JProperty("margins", this.margins.GetExportData())
				))
			);
		}
	}
}
