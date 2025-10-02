using System;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

namespace Project.PDFGenerator.Server
{
	public static class WebRequest
	{
		public static void Get(string url, string token, Action<UnityWebRequest> callback)
		{
			UnityWebRequest request = UnityWebRequest.Get(url);
			request.SetRequestHeader("Authorization", "Bearer " + token);
			request.SendWebRequest().completed += (asyncOperation) =>
			{
				callback?.Invoke(request);
				request.Dispose();
			};
		}

		public static void Post(string url, string token, string jsonData, Action<UnityWebRequest> callback)
		{
			UnityWebRequest request = new(url, UnityWebRequest.kHttpVerbPOST);
			if (!string.IsNullOrEmpty(jsonData))
			{
				byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
				request.uploadHandler = new UploadHandlerRaw(bodyRaw);
			}
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/json");
			request.SetRequestHeader("Authorization", "Bearer " + token);
			request.SendWebRequest().completed += (asyncOperation) =>
			{
				callback?.Invoke(request);
				request.Dispose();
			};
		}

		public static void Post(string url, string token, JObject data, Action<UnityWebRequest> callback)
		{
			string jsonData = data?.ToString(Newtonsoft.Json.Formatting.None);
			Post(url, token, jsonData, callback);
		}
	}
}
