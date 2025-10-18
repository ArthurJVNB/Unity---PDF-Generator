using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public class Border<TParent> : IExportable<JProperty>
	{
		public const int k_DefaultWidth = 1;
		public const BorderType k_DefaultType = BorderType.Solid;
		public readonly static Color k_DefaultColor = Color.black;

		private const string px = PDFConstants.k_Pixel;

		public int width = k_DefaultWidth;
		public BorderType type = k_DefaultType;
		public Color color = k_DefaultColor;

		internal TParent _parent;

		public Border(TParent parent)
		{
			_parent = parent;
		}

		public Border<TParent> SetBorder(int width, BorderType type, Color color)
		{
			SetWidth(width);
			SetType(type);
			SetColor(color);
			return this;
		}

		public Border<TParent> SetDefaultValues()
		{
			SetWidth(k_DefaultWidth);
			SetType(k_DefaultType);
			SetColor(k_DefaultColor);
			return this;
		}

		public Border<TParent> SetWidth(int width)
		{
			this.width = width;
			return this;
		}

		public Border<TParent> SetType(BorderType type)
		{
			this.type = type;
			return this;
		}

		public Border<TParent> SetColor(Color color)
		{
			this.color = color;
			return this;
		}

		public TParent DoneBorder()
		{
			return _parent;
		}

		public JProperty GetExportData()
		{
			return new JProperty("border", $"{width}{px} {type.ToString().ToLower()} {PDFColorUtility.ToHtmlStringRGB(color)}"); // e.g. "1px solid #000000"
		}
	}
}