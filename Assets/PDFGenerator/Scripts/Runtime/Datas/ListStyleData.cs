using System;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	[Serializable]
	public class ListStyleData : BaseStyleData<PDFListData>
	{
		//public ListStyleType listStyleType = ListStyleType.Disc; // not implemented

		public ListStyleData(PDFListData parent) : base(parent) { }
		public ListStyleData(PDFListData parent, int fontSize) : base(parent, fontSize) { }

		public override JObject GetExportData()
		{
			var export = base.GetExportData();
			export.Add("list-style-type", "disc"); // this style was not implemented
			return export;
		}
	}
}
