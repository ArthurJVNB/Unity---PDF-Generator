using System;

namespace Project.PDFGenerator
{
	[Serializable]
	public class TextStyleData : BaseStyleData<PDFTextData>
	{
		public TextStyleData(PDFTextData parent) : base(parent) { }
		public TextStyleData(PDFTextData parent, int fontSize) : base(parent, fontSize) { }
	}
}
