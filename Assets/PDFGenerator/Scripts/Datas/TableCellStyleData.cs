using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public class TableCellStyleData : IExportable<JProperty>
	{
		public Border<TableCellStyleData> border;
		public int padding;

		internal PDFTableData _parent;

		public TableCellStyleData(PDFTableData parent)
		{
			_parent = parent;
			border = new(this);
		}

		public TableCellStyleData SetPadding(int padding)
		{
			this.padding = padding;
			return this;
		}

		public TableCellStyleData SetBorder(int width, Color color)
		{
			return border.SetWidth(width).SetColor(color).DoneBorder();
		}

		public PDFTableData DoneStyle()
		{
			return _parent;
		}

		public JProperty GetExportData()
		{
			return new JProperty("cellStyle", new JObject()
			{
				new JProperty(border.GetExportData()),
				new JProperty("padding", $"{padding}{PDFConstants.k_Pixel}"),
			});

			//return new JObject()
			//{
			//	{ "cellStyle", new JObject()
			//		{
			//			//{ "border", border.GetExportData()["border"] },
			//			{ border.GetExportData() },
			//			{ "padding", $"{padding}{PDFConstants.k_Pixel}" },
			//		}
			//	}
			//};
		}
	}
}
