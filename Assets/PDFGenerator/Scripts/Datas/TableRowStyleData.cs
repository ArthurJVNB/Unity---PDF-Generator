using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public class TableRowStyleData : IExportable<JObject>
	{
		[ColorUsage(true)]
		public List<Color> backgroundColor = new() { new(0, 0, 0, 0) };

		internal PDFTableData _parent;

		private bool _isDefaultBackgroundColor = true;

		public TableRowStyleData(PDFTableData parent)
		{
			_parent = parent;
		}

		public TableRowStyleData SetBackgroundColor(List<Color> colors)
		{
			backgroundColor = colors;
			_isDefaultBackgroundColor = false;
			return this;
		}

		public TableRowStyleData SetBackgroundColor(params Color[] colors)
		{
			return SetBackgroundColor(new List<Color>(colors));
		}

		public TableRowStyleData AddBackgroundColor(Color color)
		{
			if (_isDefaultBackgroundColor)
			{
				backgroundColor.Clear();
				_isDefaultBackgroundColor = false;
			}
			backgroundColor.Add(color);
			return this;
		}

		public PDFTableData DoneStyle()
		{
			return _parent;
		}

		public JObject GetExportData()
		{
			JArray array = new();
			foreach (Color color in backgroundColor)
			{
				array.Add(new JProperty("background-color", ColorUtility.ToHtmlStringRGBA(color)));
			}

			return new JObject()
			{
				{ "rowStyle", array }
			};
		}
	}
}
