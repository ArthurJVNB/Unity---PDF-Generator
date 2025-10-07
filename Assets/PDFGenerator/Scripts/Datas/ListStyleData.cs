using System;

namespace Project.PDFGenerator
{
	[Serializable]
	public class ListStyleData : BaseStyleData<PDFListData>
	{
		public ListStyleData(PDFListData parent, int fontSize) : base(parent, fontSize) { }
	}
}
