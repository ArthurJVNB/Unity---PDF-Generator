using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public class Border<TParent> : IExportable<JProperty>
	{
		private const string px = PDFConstants.k_Pixel;

		public int width = 1;
		public BorderType type = BorderType.Solid;
		public Color color = Color.black;

		internal TParent _parent;

		public Border(TParent parent)
		{
			_parent = parent;
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
			return new JProperty("border", $"{width}{px} {type.ToString().ToLower()} {ColorUtility.ToHtmlStringRGB(color)}"); // e.g. "1px solid #000000"
		}
	}
}