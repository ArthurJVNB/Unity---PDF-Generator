using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator.Server
{
	public class PDFServer : MonoBehaviour
	{
		internal static PDFServerConfigurationData ConfigurationData { get; set; }

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		static void Init()
		{
			ConfigurationData = null;
		}

		public static void Send(PDFJObjectFactory pdfData, Action<PDFCreateCallback> callback)
		{
			Send(pdfData.Create(), callback);
		}

		public static void Send(JObject pdfData, Action<PDFCreateCallback> callback)
		{
			Send(pdfData.ToString(Formatting.None), callback);
		}

		public static void Send(string pdfData, Action<PDFCreateCallback> callback)
		{
			WWWForm form = new();
			form.AddField("json", pdfData);
			WebRequest.Post(ConfigurationData.PostCreatePdfFullPath, token: null, form, request => callback?.Invoke(new PDFCreateCallback(request)));
		}
	}
}
