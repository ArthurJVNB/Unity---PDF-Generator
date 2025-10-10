using System;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	[Serializable]
	public abstract class BasePDFData : IExportable<JObject>
	{
		public string type;
		internal PDFJObjectFactory factory;

		public BasePDFData(PDFJObjectFactory factory)
		{
			this.factory = factory;
		}

		public PDFJObjectFactory Done()
		{
			return factory;
		}

		public abstract JObject GetExportData();
	}
}
