using UnityEngine.Networking;

namespace Project.PDFGenerator.Server
{
	public abstract class ACallback
	{
		public bool Success;
		public int StatusCode;

		public ACallback(UnityWebRequest webRequest)
		{
			Success = webRequest.result == UnityWebRequest.Result.Success;
			StatusCode = (int)webRequest.responseCode;
			ResolveData(webRequest.downloadHandler.text);
		}

		protected virtual void ResolveData(string data) { }
	}
}

