using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PageMargins : IExportable<JObject>
	{
		private const int k_DefaultMargin = 40;

		[Range(0, 100)] public int top = k_DefaultMargin;
		[Range(0, 100)] public int right = k_DefaultMargin;
		[Range(0, 100)] public int bottom = k_DefaultMargin;
		[Range(0, 100)] public int left = k_DefaultMargin;

		public JObject GetExportData()
		{
			return new JObject(
				new JProperty("top", top),
				new JProperty("right", right),
				new JProperty("bottom", bottom),
				new JProperty("left", left)
			);

			//return new JObject(
			//	new JProperty("margins", new JObject(
			//		new JProperty("top", top),
			//		new JProperty("right", right),
			//		new JProperty("bottom", bottom),
			//		new JProperty("left", left))
			//	)
			//);
		}
	}
}
