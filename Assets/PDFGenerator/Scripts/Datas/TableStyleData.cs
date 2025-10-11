using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public class TableStyleData : IExportable<JProperty>
	{
		private string px = PDFConstants.k_Pixel;

		public int widthPercent = 100;
		public Border<TableStyleData> border;
		//public BorderColapseType borderColapse = BorderColapseType.Collapse; // not implemented
		public int fontSize = 13;
		public TextAlignType textAlign = TextAlignType.Center;
		public int marginBottom = 20;

		private PDFTableData _parent;

		public TableStyleData(PDFTableData parent)
		{
			_parent = parent;
			border = new(this)
			{
				width = 1,
				type = BorderType.Solid,
				color = Color.white,
			};
		}

		public TableStyleData SetWidthPercent(int widthPercent)
		{
			this.widthPercent = widthPercent;
			return this;
		}

		public TableStyleData SetBorder(Border<TableStyleData> border)
		{
			border._parent = this;
			this.border = border;
			return this;
		}

		public Border<TableStyleData> AddBorder()
		{
			border = new(this);
			return border;
		}

		public TableStyleData SetFontSize(int fontSize)
		{
			this.fontSize = fontSize;
			return this;
		}

		public TableStyleData SetTextAlign(TextAlignType textAlign)
		{
			this.textAlign = textAlign;
			return this;
		}

		public TableStyleData SetMarginBottom(int marginBottom)
		{
			this.marginBottom = marginBottom;
			return this;
		}

		public PDFTableData DoneStyle()
		{
			return _parent;
		}

		public JProperty GetExportData()
		{
			return new JProperty("style", new JObject()
			{
				new JProperty("width", $"{widthPercent:0.##}%"),
				border.GetExportData(),
				new JProperty("border-collapse", "collapse"), // not implemented class
				new JProperty("font-size", $"{fontSize}{px}"),
				new JProperty("text-align", textAlign.ToString().ToLower()),
				new JProperty("margin-bottom", $"{marginBottom}{px}")
			});
		}
	}
}
