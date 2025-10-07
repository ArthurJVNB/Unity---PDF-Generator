using System;
using System.Collections.Generic;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PDFListData : BasePDFData
	{
		public bool ordered = false;
		public List<string> items;

		public PDFListData(PDFJObjectFactory factory) : base(factory)
		{
			type = PDFConstants.k_ListType;
			items = new List<string>();
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
	}

	public class ListStyle
	{
		public int fontSize = 13;
		//public 
	}
}
