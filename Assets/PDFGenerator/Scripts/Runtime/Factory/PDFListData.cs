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
		}

		public PDFListData SetId(string id)
		{
			this.id = id;
			return this;
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

		public PDFListData AddItems(params string[] items)
		{
			this.items.AddRange(items);
			return this;
		}

		public PDFListData SetItems(params string[] items)
		{
			this.items = new List<string>(items);
			return this;
		}

		public PDFListData SetItems(List<string> items)
		{
			this.items = items;
			return this;
		}

		public ListStyleData AddStyle()
		{
			style = new(this);
			return style;
		}

		public ListStyleData AddStyle(int fontSize)
		{
			return AddStyle().SetFontSize(fontSize);
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
				new JProperty("ordered", ordered),
				new JProperty("items", items),
				new JProperty("style", style?.GetExportData() ?? new JObject()),
			};
		}
	}
}
