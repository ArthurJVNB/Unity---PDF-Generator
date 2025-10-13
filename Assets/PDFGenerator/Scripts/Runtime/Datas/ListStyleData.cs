using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public class ListStyleData : BaseStyleData<PDFListData>
	{
		public ListStyleType listStyleType = ListStyleType.None;

		public ListStyleData(PDFListData parent) : base(parent) { }
		public ListStyleData(PDFListData parent, int fontSize) : base(parent, fontSize) { }

		public ListStyleData SetListStyleType(ListStyleType style)
		{
			this.listStyleType = style;
			return this;
		}

		public new ListStyleData SetFontSize(int fontSize) => (ListStyleData)base.SetFontSize(fontSize);
		public new ListStyleData SetFontStyle(TextFontStyleType fontStyle) => (ListStyleData)base.SetFontStyle(fontStyle);
		public new ListStyleData SetTextAlign(TextAlignType textAlign) => (ListStyleData)base.SetTextAlign(textAlign);
		public new ListStyleData SetFontWeight(FontWeight fontWeight) => (ListStyleData)base.SetFontWeight(fontWeight);
		public new ListStyleData SetLineHeight(float lineHeight) => (ListStyleData)base.SetLineHeight(lineHeight);
		public new ListStyleData SetColor(Color color) => (ListStyleData)base.SetColor(color);
		public new ListStyleData SetMarginTop(int marginTop) => (ListStyleData)base.SetMarginTop(marginTop);
		public new ListStyleData SetMarginBottom(int marginBottom) => (ListStyleData)base.SetMarginBottom(marginBottom);
		public new ListStyleData SetMarginLeft(int marginLeft) => (ListStyleData)base.SetMarginLeft(marginLeft);
		public new ListStyleData SetMarginRight(int marginRight) => (ListStyleData)base.SetMarginRight(marginRight);

		public override JObject GetExportData()
		{
			var export = base.GetExportData();
			if (listStyleType != ListStyleType.None) export.Add("list-style-type", listStyleType.ToString().ToLower());

			return export;
		}
	}
}
