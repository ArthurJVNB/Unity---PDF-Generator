using System;
using UnityEngine;

namespace Project.PDFGenerator
{
	[Serializable]
	public class ImageData
	{
		public string url;
		public Vector2 size = new(100, 100);
	}
}
