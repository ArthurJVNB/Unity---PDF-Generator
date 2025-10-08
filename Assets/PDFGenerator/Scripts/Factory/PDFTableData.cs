using System;
using System.Collections.Generic;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PDFTableData : BasePDFData
	{
		public List<string> header = new();
		public List<List<string>> rows = new();
		public TableStyleData style;
		//TODO: headerStyle
		//TODO: rowStyle

		public PDFTableData(PDFJObjectFactory factory) : base(factory)
		{
			type = PDFConstants.k_TableType;
			style = new(this);
		}

		public PDFTableData SetHeader(List<string> header)
		{
			this.header = header;
			return this;
		}

		public PDFTableData SetHeader(params string[] header)
		{
			return SetHeader(new List<string>(header));
		}

		public PDFTableData AddRow(List<string> row)
		{
			rows.Add(row);
			return this;
		}

		public PDFTableData AddRow(params string[] row)
		{
			return AddRow(new List<string>(row));
		}

		//TODO: Export (JObject)
	}
}
