using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PDFListData : BasePDFData
	{
		public bool ordered = false;
		public List<string> items = new();
		public ListStyleData style;

		public PDFListData(PDFJObjectFactory factory) : base(factory)
		{
			type = PDFConstants.k_ListType;
			style = new(this);
		}

		public PDFListData SetOrdered(bool ordered)
		{
			this.ordered = ordered;
			return this;
		}

		public PDFListData AddItem(string item)
		{
			items.Add(item);
			return this;
		}

		public ListStyleData AddStyle(int fontSize)
		{
			//style = new(this); // not necessary
			style.fontSize = fontSize;
			return style;
		}

		public override JObject GetExportData()
		{
			var items = new JArray();
			foreach (var item in this.items)
			{
				items.Add(item);
			}
			return new JObject()
			{
				new JProperty("type", type),
				new JProperty("ordered", ordered.ToString().ToLower()),
				new JProperty("items", items),
				new JProperty("style", style.GetExportData()),
			};
		}
	}
}
