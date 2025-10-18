using System;

namespace Project.PDFGenerator
{
	[Serializable]
	public class TextStyleData : BaseStyleData<PDFTextData>
	{
		#region Backup
		//// Mandatory
		//public int fontSize;

		//// Optional
		//public TextFontStyleType fontStyle = TextFontStyleType.Normal;
		//public TextAlignType textAlign = TextAlignType.Left;
		//public int lineHeight;
		//public Color color = Color.black;
		//public int marginTop;
		//public int marginBottom;

		//public bool useFontStyle = false;
		//public bool useTextAlign = false;
		//public bool useLineHeight = false;
		//public bool useColor = false;
		//public bool useMarginTop = false;
		//public bool useMarginBottom = false;

		//internal PDFTextData _textData;

		//internal TextStyleData(PDFTextData textData)
		//{
		//	_textData = textData;
		//}

		//public TextStyleData(PDFTextData textData, int fontSize) : this(textData)
		//{
		//	this.fontSize = fontSize;
		//}

		//public TextStyleData SetFontStyle(TextFontStyleType fontStyle)
		//{
		//	this.fontStyle = fontStyle;
		//	useFontStyle = true;
		//	return this;
		//}

		//public TextStyleData SetTextAlign(TextAlignType textAlign)
		//{
		//	this.textAlign = textAlign;
		//	useTextAlign = true;
		//	return this;
		//}

		//public TextStyleData SetLineHeight(int lineHeight)
		//{
		//	this.lineHeight = lineHeight;
		//	useLineHeight = true;
		//	return this;
		//}

		//public TextStyleData SetColor(Color color)
		//{
		//	this.color = color;
		//	useColor = true;
		//	return this;
		//}

		//public TextStyleData SetMarginTop(int marginTop)
		//{
		//	this.marginTop = marginTop;
		//	useMarginTop = true;
		//	return this;
		//}

		//public TextStyleData SetMarginBottom(int marginBottom)
		//{
		//	this.marginBottom = marginBottom;
		//	useMarginBottom = true;
		//	return this;
		//}

		//public PDFTextData EndStyle()
		//{
		//	return _textData;
		//}

		//public JObject GetExportData()
		//{
		//	var style = new JObject
		//	{
		//		["fontSize"] = fontSize
		//	};

		//	if (useFontStyle) style.Add("font-style", fontStyle.ToString().ToLower());
		//	if (useTextAlign) style.Add("text-align", textAlign.ToString().ToLower());
		//	if (useLineHeight) style.Add("line-height", lineHeight);
		//	if (useColor) style.Add("color", ColorUtility.ToHtmlStringRGB(color));
		//	if (useMarginTop) style.Add("margin-top", marginTop);
		//	if (useMarginBottom) style.Add("margin-bottom", marginBottom);

		//	return new JObject { { "style", style } };
		//}
		#endregion
		
		public TextStyleData(PDFTextData parent) : base(parent) { }
		public TextStyleData(PDFTextData parent, int fontSize) : base(parent, fontSize) { }
	}
}
