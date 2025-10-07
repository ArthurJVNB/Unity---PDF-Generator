using System;

namespace Project.PDFGenerator
{
	[Serializable]
	public abstract class BasePDFData
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
	}
}
