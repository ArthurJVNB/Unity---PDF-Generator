using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Project.PDFGenerator
{
	public class PDFJObjectFactory
	{
		private PageData m_Page;
		private List<BasePDFData> m_Datas;

		public static PageData Start()
		{
			var factory = new PDFJObjectFactory(new PageData());
			return factory.m_Page;
		}

		public static PDFJObjectFactory Start(PageData page)
		{
			return new PDFJObjectFactory(page);
		}

		private PDFJObjectFactory(PageData page)
		{
			page._parent = this;
			m_Page = page;
			m_Datas = new List<BasePDFData>();
		}

		#region AddText
		public PDFTextData AddText()
		{
			var data = new PDFTextData(this);
			m_Datas.Add(data);
			return data;
		}

		public PDFTextData AddText(string text)
		{
			return AddText().SetText(text);
		}

		public PDFTextData AddText(TextType type, string text)
		{
			return AddText(text).SetType(type);
		}

		public PDFTextData AddText(TextType type)
		{
			return AddText().SetType(type);
		}
		#endregion

		#region AddImage
		public PDFImageData AddImage(string url)
		{
			return AddImage().SetUrl(url);
		}

		public PDFImageData AddImage(string url, Rect rect)
		{
			return AddImage(url).SetPosition(rect.x, rect.y).SetSize(rect.width, rect.height);
		}

		public PDFImageData AddImage(Rect rect)
		{
			return AddImage().SetPosition(rect.x, rect.y).SetSize(rect.width, rect.height);
		}

		public PDFImageData AddImage(float x, float y)
		{
			return AddImage().SetPosition(x, y);
		}

		public PDFImageData AddImage()
		{
			var data = new PDFImageData(this);
			m_Datas.Add(data);
			return data;
		}
		#endregion

		#region AddList
		public PDFListData AddList()
		{
			var data = new PDFListData(this);
			m_Datas.Add(data);
			return data;
		}
		#endregion

		#region AddTable (TODO)
		//TODO
		#endregion

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
