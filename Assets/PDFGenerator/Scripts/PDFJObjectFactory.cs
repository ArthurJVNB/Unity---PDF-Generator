using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	public class PDFJObjectFactory
	{
		private PageData m_Page;
		private List<BasePDFData> m_Datas;

		public static PDFJObjectFactory Start(PageData page)
		{
			return new PDFJObjectFactory(page);
		}

		private PDFJObjectFactory(PageData page)
		{
			m_Page = page;
			m_Datas = new List<BasePDFData>();
		}

		public PDFTextData AddText(float x, float y)
		{
			return (PDFTextData)AddText().SetPosition(x, y);
		}

		public PDFTextData AddText()
		{
			var data = new PDFTextData(this);
			m_Datas.Add(data);
			return data;
		}

		public PDFImageData AddImage(string url)
		{
			return AddImage().SetUrl(url);
		}

		public PDFImageData AddImage(float x, float y)
		{
			return (PDFImageData)AddImage().SetPosition(x, y);
		}

		public PDFImageData AddImage()
		{
			var data = new PDFImageData(this);
			m_Datas.Add(data);
			return data;
		}

		public (JObject page, JObject[] content) Create()
		{
			(JObject page, JObject[] content) result = new();
			result.page = m_Page.GetExportData();
			result.content = new JObject[m_Datas.Count];
			for (int i = 0; i < m_Datas.Count; i++)
			{
				result.content[i] = JObject.FromObject(m_Datas[i]);
			}
			return result;
		}
	}
}
