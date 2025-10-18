using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.PDFGenerator.Server
{
	[Serializable]
	public class PDFCreateCallback : ACallback
	{
		public string PDFUrl;

		public PDFCreateCallback(UnityWebRequest webRequest) : base(webRequest)
		{
			if (!Success)
				Debug.LogError("Error trying to generate PDF: " + webRequest.error);
		}

		protected override void ResolveData(string data)
		{
			base.ResolveData(data);

			if (string.IsNullOrEmpty(data)) return;
			PDFUrl = data.TrimStart('\uFEFF').Trim();

			Debug.Log("PDF URL: " + PDFUrl);
		}
	}
}
