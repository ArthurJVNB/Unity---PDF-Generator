using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator.Server
{
	[Serializable]
	internal class PDFSendRequestData
	{
		[TextArea(1, 32)]
		[SerializeField] private string _data;

		public string Data => _data;

		public void SetData(PDFJObjectFactory pdfData)
		{
			SetData(pdfData?.Create());
		}

		public void SetData(JObject pdfData)
		{
			SetData(pdfData?.ToString());
		}

		public void SetData(string data)
		{
			_data = data;
		}
	}
}