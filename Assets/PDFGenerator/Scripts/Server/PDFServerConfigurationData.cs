using UnityEngine;

namespace Project.PDFGenerator.Server
{
	[CreateAssetMenu(fileName = "PDFServerConfigurationData", menuName = "PDF/Server Configuration")]
	public class PDFServerConfigurationData : ScriptableObject
	{
		[SerializeField] private string _serverURL;

		public string ServerURL => _serverURL;
	}
}
