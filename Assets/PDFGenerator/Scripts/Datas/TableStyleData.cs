using System;

namespace Project.PDFGenerator
{
	[Serializable]
	public class TableStyleData : BaseStyleData<PDFTableData>
	{
		public TableStyleData(PDFTableData parent) : base(parent) { }
		public TableStyleData(PDFTableData parent, int fontSize) : base(parent, fontSize) { }
	}
}
