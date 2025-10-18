using UnityEngine;

namespace Project.PDFGenerator.Server
{
	[CreateAssetMenu(fileName = "PDFServerConfigurationData", menuName = "PDF/Server Configuration")]
	public class PDFServerConfigurationData : ScriptableObject
	{
		[SerializeField] private string _api;
		[SerializeField] private string _postCreatePdf;

		public string API => _api;
		public string PostCreatePdf => _postCreatePdf;
		public string PostCreatePdfFullPath => $"{API.TrimEnd('/')}/{PostCreatePdf.TrimStart('/')}";
	}
}
