using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Project.PDFGenerator.Server
{
	public class PDFSendBehaviour : MonoBehaviour
	{
		public event Action OnRequestStarted;
		public event Action<PDFCreateCallback> OnRequestComplete;
		public event Action<PDFCreateCallback> OnRequestSuccess;
		public event Action<PDFCreateCallback> OnRequestFailure;

		[SerializeField] private PDFServerConfigurationData _serverConfigurationData;
		[SerializeField] private SuccessBehaviourType _successBehaviourType = SuccessBehaviourType.EventsOnly;
		[Tooltip("Optional.\n\nStored data that can later be used to send request.")]
		[ContextMenuItem("Debug/Send PDF Request Stored Data", nameof(Debug_SendPDFRequestStoredData))]
		[ContextMenuItem("Debug/Send PDF Request Stored Data (Using Callbacks) (Play Mode)", nameof(Debug_SendPDFRequestStoredData_UseCallback))]
		[SerializeField] private PDFSendRequestData _storedData;
		[Tooltip("Last PDF URL received from server. It will always be replaced by the newest request. " +
			"So, if the last connection failed, the previous successful PDF will be lost.")]
		[ContextMenuItem("Open Last PDF", nameof(OpenLastPDF))]
		[SerializeField] private string _lastPDFUrl;

		[Header("Events")]
		[SerializeField] private UnityEvent _onRequestStarted;
		[SerializeField] private UnityEvent<PDFCreateCallback> _onRequestComplete;
		[SerializeField] private UnityEvent<PDFCreateCallback> _onRequestSuccess;
		[SerializeField] private UnityEvent<PDFCreateCallback> _onRequestFailure;

		private enum SuccessBehaviourType
		{
			EventsOnly,
			EventsAndOpenUrl,
		}

		public string LastPDFUrl => _lastPDFUrl;

		[ContextMenu("Open Last PDF")]
		public void OpenLastPDF()
		{
			Application.OpenURL(_lastPDFUrl);
		}

		#region Public: Set Stored Data
		public void SetStoredData(PDFJObjectFactory pdfData)
		{
			_storedData.SetData(pdfData);
		}

		public void SetStoredData(JObject pdfData)
		{
			_storedData.SetData(pdfData);
		}

		public void SetStoredData(string pdfData)
		{
			_storedData.SetData(pdfData);
		}
		#endregion

		#region Public: Send PDF Request
		public void SendPDFRequest(PDFJObjectFactory pdfData) => SendPDFRequest(pdfData, null);
		public void SendPDFRequest(PDFJObjectFactory pdfData, Action<PDFCreateCallback> callback)
		{
			SetupStartRequest();
			PDFServer.Send(pdfData, requestCallback => Callback(requestCallback, callback));
		}

		public void SendPDFRequest(JObject pdfData) => SendPDFRequest(pdfData, null);
		public void SendPDFRequest(JObject pdfData, Action<PDFCreateCallback> callback)
		{
			SetupStartRequest();
			PDFServer.Send(pdfData, requestCallback => Callback(requestCallback, callback));
		}

		public void SendPDFRequest(string pdfData) => SendPDFRequest(pdfData, null);
		public void SendPDFRequest(string pdfData, Action<PDFCreateCallback> callback)
		{
			SetupStartRequest();
			PDFServer.Send(pdfData, requestCallback => Callback(requestCallback, callback));
		}

		public void SendPDFRequestStoredData() => SendPDFRequestStoredData(null);
		public void SendPDFRequestStoredData(Action<PDFCreateCallback> callback)
		{
			SetupStartRequest();
			SendPDFRequest(_storedData.Data, callback);
		}
		#endregion

		#region Private
		private void SetupStartRequest()
		{
			PDFServer.ConfigurationData = _serverConfigurationData;
			InvokeStartEvent();
		}

		private void Callback(PDFCreateCallback requestCallback, Action<PDFCreateCallback> callback)
		{
			_lastPDFUrl = requestCallback.PDFUrl;

			callback?.Invoke(requestCallback);
			InvokeCallbackEvents(requestCallback);

			if (!requestCallback.Success) return;
			switch (_successBehaviourType)
			{
				case SuccessBehaviourType.EventsAndOpenUrl:
					OpenLastPDF();
					break;
				case SuccessBehaviourType.EventsOnly:
				default:
					break;
			}
		}

		private void InvokeStartEvent()
		{
			OnRequestStarted?.Invoke();
			_onRequestStarted?.Invoke();
		}

		private void InvokeCallbackEvents(PDFCreateCallback callback)
		{
			OnRequestComplete?.Invoke(callback);
			_onRequestComplete?.Invoke(callback);
			if (callback.Success)
			{
				OnRequestSuccess?.Invoke(callback);
				_onRequestSuccess?.Invoke(callback);
			}
			else
			{
				OnRequestFailure?.Invoke(callback);
				_onRequestFailure?.Invoke(callback);
			}
		}
		#endregion

		#region Debug
		[ContextMenu("Debug/Send PDF Request Stored Data/No Callbacks")]
		private void Debug_SendPDFRequestStoredData()
		{
			Debug.Log("<color=yellow>Debug:</color> Send PDF Request Stored Data");
			PDFServer.ConfigurationData = _serverConfigurationData;
			PDFServer.Send(_storedData.Data, callback =>
			{
				Debug.Log($"<color=yellow>Debug:</color> PDF Request Completed. Success: {callback.Success}. Url: {callback.PDFUrl}");
				if (callback.Success) Application.OpenURL(callback.PDFUrl);
				PDFServer.ConfigurationData = null;
			});
		}

		[ContextMenu("Debug/Send PDF Request Stored Data/Using Callbacks (Play Mode)")]
		private void Debug_SendPDFRequestStoredData_UseCallback()
		{
			if (!Application.isPlaying) return;
			Debug.Log("<color=yellow>Debug:</color> Send PDF Request Stored Data (Using Callbacks)");
			SendPDFRequestStoredData(callback =>
			{
				Debug.Log($"<color=yellow>Debug:</color> PDF Request Completed. Success: {callback.Success}. Url: {callback.PDFUrl}");
				PDFServer.ConfigurationData = null;
			});
		}

		[ContextMenu("Debug/Send PDF Request Factory/No Callbacks")]
		private void Debug_SendPDFRequestFactory()
		{
			Debug.Log("<color=yellow>Debug:</color> Send PDF Request Stored Data");
			PDFServer.ConfigurationData = _serverConfigurationData;

			PDFJObjectFactory factory = PDFJObjectFactory.Start(PageSize.A4, PageOrientation.Portrait)
				.AddText(TextType.Title, "Debug PDF (PDFSendBehaviour)").Done()
				.AddText(TextType.Paragraph, "This is a debug paragraph added to the PDF, sent from PDFSendBehaviour.").Done();

			//PDFJObjectFactory factory = PDFJObjectFactory.Start(PageSize.A4, PageOrientation.Portrait)
			//	.AddText(TextType.Title, "Relatório de Vendas (usando PDFSendBehaviour)")
			//		.AddStyle(24).SetTextAlign(TextAlignType.Center).SetColor(PDFColorUtility.ParseHtmlString("#2E86C1")).DoneStyle()
			//		.Done()
			//	.AddText(TextType.Paragraph, "Teste Gerando via Unity + PHP usando PDFSendBehaviour")
			//		.AddStyle(14).SetMarginBottom(10).DoneStyle()
			//		.Done()
			//	.AddImage("http://www.propixelgames.online/pdf/images/icon.png")
			//		.AddStyle().SetDisplay(ImageDisplayType.Block).SetMargin(10, marginAuto: true).DoneStyle()
			//		.Done()
			//	.AddTable()
			//		.SetHeader("Produto", "Quantidade", "Preço")
			//		.AddRow("Nintendo Switch", "10", "R$2000")
			//		.AddRow("Smash bross ultimate", "5", "R$400")
			//		.AddRow("Pro Controller", "8", "R$300")
			//		.AddStyle().SetWidthPercent(100).SetBorder(1, Color.black, BorderType.Solid).DoneStyle()
			//		.Done();

			//SetStoredData(factory);

			PDFServer.Send(factory, callback =>
			{
				Debug.Log($"<color=yellow>Debug:</color> PDF Request Completed. Success: {callback.Success}. Url: {callback.PDFUrl}");
				if (callback.Success) Application.OpenURL(callback.PDFUrl);
				PDFServer.ConfigurationData = null;
			});
		}

		[ContextMenu("Debug/Send PDF Request Factory/No Callbacks (Save on Stored Data)")]
		private void Debug_SendPDFRequestFactory_SaveOnStoredData()
		{
			Debug.Log("<color=yellow>Debug:</color> Send PDF Request Stored Data");
			PDFServer.ConfigurationData = _serverConfigurationData;

			PDFJObjectFactory factory = PDFJObjectFactory.Start(PageSize.A4, PageOrientation.Portrait)
				.AddText(TextType.Title, "Debug PDF (PDFSendBehaviour)").Done()
				.AddText(TextType.Paragraph, "This is a debug paragraph added to the PDF, sent from PDFSendBehaviour.").Done();

			//PDFJObjectFactory factory = PDFJObjectFactory.Start(PageSize.A4, PageOrientation.Portrait)
			//	.AddText(TextType.Title, "Relatório de Vendas (usando PDFSendBehaviour)")
			//		.AddStyle(24).SetTextAlign(TextAlignType.Center).SetColor(PDFColorUtility.ParseHtmlString("#2E86C1")).DoneStyle()
			//		.Done()
			//	.AddText(TextType.Paragraph, "Teste Gerando via Unity + PHP usando PDFSendBehaviour")
			//		.AddStyle(14).SetMarginBottom(10).DoneStyle()
			//		.Done()
			//	.AddImage("http://www.propixelgames.online/pdf/images/icon.png")
			//		.AddStyle().SetDisplay(ImageDisplayType.Block).SetMargin(10, marginAuto: true).DoneStyle()
			//		.Done()
			//	.AddTable()
			//		.SetHeader("Produto", "Quantidade", "Preço")
			//		.AddRow("Nintendo Switch", "10", "R$2000")
			//		.AddRow("Smash bross ultimate", "5", "R$400")
			//		.AddRow("Pro Controller", "8", "R$300")
			//		.AddStyle().SetWidthPercent(100).SetBorder(1, Color.black, BorderType.Solid).DoneStyle()
			//		.Done();

			SetStoredData(factory);

			PDFServer.Send(factory, callback =>
			{
				Debug.Log($"<color=yellow>Debug:</color> PDF Request Completed. Success: {callback.Success}. Url: {callback.PDFUrl}");
				if (callback.Success) Application.OpenURL(callback.PDFUrl);
				PDFServer.ConfigurationData = null;
			});
		}
		#endregion
	}
}
