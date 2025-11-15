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

		public PDFTableData SetId(string id)
		{
			this.id = id;
			return this;
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

		public PDFTableData AddHeader(string header)
		{
			this.header.Add(header);
			return this;
		}

		public PDFTextData AddHeaders(params string[] headers)
		{
			header.AddRange(headers);
			return null;
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

		public TableStyleData AddStyle(int fontSize)
		{
			return style.SetFontSize(fontSize);
		}

		public PDFTableData SetStyle(TableStyleData style)
		{
			style._parent = this;
			this.style = style;
			return this;
		}

		public TableHeaderStyleData AddHeaderStyle()
		{
			return headerStyle;
		}

		public PDFTableData SetHeaderStyle(TableHeaderStyleData headerStyle)
		{
			headerStyle._parent = this;
			this.headerStyle = headerStyle;
			return this;
		}

		public TableCellStyleData AddCellStyle()
		{
			return cellStyle;
		}

		public PDFTableData SetCellStyle(TableCellStyleData cellStyle)
		{
			cellStyle._parent = this;
			this.cellStyle = cellStyle;
			return this;
		}

		public TableRowStyleData AddRowStyle()
		{
			return rowStyle;
		}

		public PDFTableData SetRowStyle(TableRowStyleData rowStyle)
		{
			rowStyle._parent = this;
			this.rowStyle = rowStyle;
			return this;
		}

		public override JObject GetExportData()
		{
			var rows = new JArray();
			foreach (var row in this.rows)
				rows.Add(new JArray(row.ToArray()));
			return new JObject()
			{
				new JProperty("type", type),
				new JProperty("header", new JArray(header.ToArray())),
				//new JProperty("rows", new JArray(rows.ConvertAll(row => new JArray(row)))),
				//new JProperty("style", style.GetExportData()),
				//new JProperty("headerStyle", headerStyle.GetExportData()),
				//new JProperty("cellStyle", cellStyle.GetExportData()),
				//new JProperty("rowStyle", rowStyle.GetExportData()),
				new JProperty("rows", rows),
				style.GetExportData(),
				headerStyle.GetExportData(),
				cellStyle.GetExportData(),
				rowStyle.GetExportData(),
			};
		}
	}
}
