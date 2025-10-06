using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PageMargins : IExportable<JObject>
	{
		[Range(0, 100)] public int top = 40;
		[Range(0, 100)] public int right = 40;
		[Range(0, 100)] public int bottom = 40;
		[Range(0, 100)] public int left = 40;

		public JObject GetExportData()
		{
			return new JObject(
				new JProperty("top", top),
				new JProperty("right", right),
				new JProperty("bottom", bottom),
				new JProperty("left", left)
			);

			return new JObject(
				new JProperty("margins", new JObject(
					new JProperty("top", top),
					new JProperty("right", right),
					new JProperty("bottom", bottom),
					new JProperty("left", left))
				)
			);
		}
	}
}
