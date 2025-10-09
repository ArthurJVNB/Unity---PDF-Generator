using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	public class BaseStyleData<TParent> : IExportable<JObject>
	{
		// Mandatory
		public int fontSize;

		// Optional
		public TextFontStyleType fontStyle = TextFontStyleType.Normal;
		public TextAlignType textAlign = TextAlignType.Left;
		public FontWeight fontWeight = FontWeight.Regular;
		public float lineHeight;
		public Color color = Color.black;
		public int marginTop;
		public int marginBottom;

		public bool useFontStyle = false;
		public bool useTextAlign = false;
		public bool useFontWeight = false;
		public bool useLineHeight = false;
		public bool useColor = false;
		public bool useMarginTop = false;
		public bool useMarginBottom = false;

		protected TParent _parent;

		public BaseStyleData(TParent parent)
		{
			_parent = parent;
			this.fontSize = PDFConstants.k_DefaultFontSize;
		}

		public BaseStyleData(TParent parent, int fontSize) : this(parent)
		{
			this.fontSize = fontSize;
		}

		public BaseStyleData<TParent> SetFontStyle(TextFontStyleType fontStyle)
		{
			this.fontStyle = fontStyle;
			useFontStyle = true;
			return this;
		}

		public BaseStyleData<TParent> SetTextAlign(TextAlignType textAlign)
		{
			this.textAlign = textAlign;
			useTextAlign = true;
			return this;
		}

		public BaseStyleData<TParent> SetFontWeight(FontWeight fontWeight)
		{
			this.fontWeight = fontWeight;
			useFontWeight = true;
			return this;
		}

		public BaseStyleData<TParent> SetLineHeight(float lineHeight)
		{
			this.lineHeight = lineHeight;
			useLineHeight = true;
			return this;
		}

		public BaseStyleData<TParent> SetColor(Color color)
		{
			this.color = color;
			useColor = true;
			return this;
		}

		public BaseStyleData<TParent> SetMarginTop(int marginTop)
		{
			this.marginTop = marginTop;
			useMarginTop = true;
			return this;
		}

		public BaseStyleData<TParent> SetMarginBottom(int marginBottom)
		{
			this.marginBottom = marginBottom;
			useMarginBottom = true;
			return this;
		}

		public TParent DoneStyle()
		{
			return _parent;
		}

		public JObject GetExportData()
		{
			var style = new JObject
			{
				["fontSize"] = fontSize
			};

			if (useFontStyle) style.Add("font-style", fontStyle.ToString().ToLower());
			if (useTextAlign) style.Add("text-align", textAlign.ToString().ToLower());
			if (useFontWeight) style.Add("font-weight", fontWeight.ToString().ToLower());
			if (useLineHeight) style.Add("line-height", lineHeight);
			if (useColor) style.Add("color", ColorUtility.ToHtmlStringRGB(color));
			if (useMarginTop) style.Add("margin-top", marginTop);
			if (useMarginBottom) style.Add("margin-bottom", marginBottom);

			return new JObject { { "style", style } };
		}
	}
}
