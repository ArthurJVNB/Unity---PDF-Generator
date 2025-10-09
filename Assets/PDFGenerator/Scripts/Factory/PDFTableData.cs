using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	[Serializable]
	public class PDFTableData : BasePDFData, IExportable<JObject>
	{
		public List<string> header = new();
		public List<List<string>> rows = new();
		public TableStyleData style;
		public TableHeaderStyleData headerStyle;
		public TableCellStyleData cellStyle;
		public TableRowStyleData rowStyle;

		public PDFTableData(PDFJObjectFactory factory) : base(factory)
		{
			type = PDFConstants.k_TableType;
			style = new(this);
			headerStyle = new(this);
			cellStyle = new(this);
			rowStyle = new(this);
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

		public TableStyleData AddStyle()
		{
			return style;
		}

		public TableHeaderStyleData AddHeaderStyle()
		{
			return headerStyle;
		}

		public TableCellStyleData AddCellStyle()
		{
			return cellStyle;
		}

		public TableRowStyleData AddRowStyle()
		{
			return rowStyle;
		}

		public JObject GetExportData()
		{
			return new JObject()
			{
				new JProperty("type", type),
				new JProperty("header", new JArray(header)),
				new JProperty("rows", new JArray(rows.ConvertAll(row => new JArray(row)))),
				new JProperty("style", style.GetExportData()),
				new JProperty("headerStyle", headerStyle.GetExportData()),
				new JProperty("cellStyle", cellStyle.GetExportData()),
				new JProperty("rowStyle", rowStyle.GetExportData()),
			};
		}
	}
}
