using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public class TableHeaderStyleData : IExportable<JObject>
	{
		private const string px = PDFConstants.k_Pixel;

		public Color backgroundColor;
		public Color color;
		public FontWeight fontWeight = FontWeight.Regular;
		public int padding;

		internal PDFTableData _parent;

		public TableHeaderStyleData(PDFTableData parent)
		{
			_parent = parent;
			backgroundColor = Color.gray;
			color = Color.white;
			fontWeight = FontWeight.Bold;
			padding = 6;
		}

		public TableHeaderStyleData SetBackgroundColor(Color color)
		{
			backgroundColor = color;
			return this;
		}

		public TableHeaderStyleData SetColor(Color color)
		{
			this.color = color;
			return this;
		}

		public TableHeaderStyleData SetFontWeight(FontWeight fontWeight)
		{
			this.fontWeight = fontWeight;
			return this;
		}

		public TableHeaderStyleData SetPadding(int padding)
		{
			this.padding = padding;
			return this;
		}

		public JObject GetExportData()
		{
			return new JObject()
			{
				{ "headerStyle", new JObject()
					{
						{ "background-color", ColorUtility.ToHtmlStringRGBA(backgroundColor) },
						{ "color", ColorUtility.ToHtmlStringRGBA(color) },
						{ "fontWeight", fontWeight.ToString().ToLower() },
						{ "padding", $"{padding}{px}"},
					}
				}
			};
		}
	}
}
