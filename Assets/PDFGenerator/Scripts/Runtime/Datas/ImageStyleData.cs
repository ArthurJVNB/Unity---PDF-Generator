using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public class ImageStyleData : IExportable<JProperty>
	{
		public const ImageDisplayType k_DefaultDisplayType = ImageDisplayType.Block;
		public const bool k_DefaultMarginAuto = true;

		private const string px = PDFConstants.k_Pixel;

		// Required
		public ImageDisplayType display = k_DefaultDisplayType;

		// Optional
		public int margin = 10;
		public bool marginAuto;
		public int width = 120;
		public int height = 120;
		public Border<ImageStyleData> border;
		public int borderRadius = 10;
		public Color backgroundColor;
		public int padding = 5;

		public bool useMargin = false;
		public bool useWidth = false;
		public bool useHeight = false;
		public bool useBorder = false;
		public bool useBorderRadius = false;
		public bool useBackgroundColor = false;
		public bool usePadding = false;

		internal PDFImageData _parent;

		public ImageStyleData() { }

		public ImageStyleData(PDFImageData parent)
		{
			_parent = parent;
			backgroundColor = PDFColorUtility.ParseHtmlString("EAF2F8");
			border = new(this)
			{
				width = 2,
				color = PDFColorUtility.ParseHtmlString("2E86C1"),
				type = BorderType.Solid,
			};
		}

		public ImageStyleData(PDFImageData parent, ImageDisplayType display, int margin, bool marginAuto, int width, int height) : this(parent)
		{
			this.display = display;
			this.margin = margin;
			this.marginAuto = marginAuto;
			this.width = width;
			this.height = height;
		}

		public ImageStyleData SetDisplay(ImageDisplayType display)
		{
			this.display = display;
			return this;
		}

		public ImageStyleData SetMargin(int margin, bool marginAuto = k_DefaultMarginAuto)
		{
			this.margin = margin;
			this.marginAuto = marginAuto;
			useMargin = true;
			return this;
		}

		public ImageStyleData SetWidth(int width)
		{
			this.width = width;
			useWidth = true;
			return this;
		}

		public ImageStyleData SetHeight(int height)
		{
			this.height = height;
			useHeight = true;
			return this;
		}

		public ImageStyleData SetSize(int width, int height)
		{
			return SetWidth(width).SetHeight(height);
		}

		public ImageStyleData SetBorder(int width, Color color, BorderType borderType = BorderType.Solid)
		{
			AddBorder().SetWidth(width).SetColor(color).SetType(borderType);
			return this;
		}

		public Border<ImageStyleData> AddBorder()
		{
			border = new(this);
			useBorder = true;
			return border;
		}

		public ImageStyleData SetBorderRadius(int borderRadius)
		{
			this.borderRadius = borderRadius;
			useBorderRadius = true;
			return this;
		}

		public ImageStyleData SetBackgroundColor(Color backgroundColor)
		{
			this.backgroundColor = backgroundColor;
			useBackgroundColor = true;
			return this;
		}

		public ImageStyleData SetPadding(int padding)
		{
			this.padding = padding;
			usePadding = true;
			return this;
		}

		public PDFImageData DoneStyle()
		{
			return _parent;
		}

		public JProperty GetExportData()
		{
			//var export = new JProperty("style", new JObject
			//{
			//	{ "display", display.ToString().ToLower() },
			//	{ "margin", $"{margin}{px}{(marginAuto ? " auto" : string.Empty)}" },
			//	{ "width", $"{width}{px}" },
			//	{ "height", $"{height}{px}" }
			//});

			var style = new JObject
			{
				{ "display", display.ToStringPDF() },
				//{ "margin", $"{margin}{px}{(marginAuto ? " auto" : string.Empty)}" },
				//{ "width", $"{width}{px}" },
				//{ "height", $"{height}{px}" }
			};

			if (useMargin) style.Add("margin", $"{margin}{px}{(marginAuto ? " auto" : string.Empty)}");
			if (useWidth) style.Add("width", $"{width}{px}");
			if (useHeight) style.Add("height", $"{height}{px}");
			if (useBorder) style.Add(border.GetExportData());
			if (useBorderRadius) style.Add("border-radius", $"{borderRadius}{px}");
			if (useBackgroundColor) style.Add("background-color", PDFColorUtility.ToHtmlStringRGB(backgroundColor));
			if (usePadding) style.Add("padding", $"{padding}{px}");

			return new JProperty("style", style);
		}
	}
}
