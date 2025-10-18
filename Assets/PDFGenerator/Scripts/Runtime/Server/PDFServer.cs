using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.PDFGenerator.Server;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.PDFGenerator.Server
{
	public class PDFServer : MonoBehaviour
	{
		//private static PDFServer _instance;
		//private static PDFServer Instance
		//{
		//	get
		//	{
		//		if (_instance == null)
		//		{
		//			var instance = new GameObject("PDFServer");
		//			_instance = instance.AddComponent<PDFServer>();
		//			DontDestroyOnLoad(instance);
		//		}
		//		return _instance;
		//	}
		//}

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
			WebRequest.Post(ConfigurationData.PostCreatePdfFullPath, token: null, pdfData, request => callback?.Invoke(new PDFCreateCallback(request)));
		}

		//private void Awake()
		//{
		//    if (_instance)
		//    {
		//        Destroy(gameObject);
		//        return;
		//    }
		//    _instance = this;
		//}
	}
}
