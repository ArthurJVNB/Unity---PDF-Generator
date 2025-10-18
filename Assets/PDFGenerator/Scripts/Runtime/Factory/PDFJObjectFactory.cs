using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
	public class PDFJObjectFactory
	{
		private readonly PageData _page;
		private readonly List<BasePDFData> _datas;

		public static PageData Start()
		{
			var factory = new PDFJObjectFactory(new PageData());
			return factory._page;
		}

		public static PDFJObjectFactory Start(PageData page)
		{
			return new PDFJObjectFactory(page);
		}

		public static PDFJObjectFactory Start(PageSize pageSize = PageData.k_DefaultSize,
										PageOrientation pageOrientation = PageData.k_DefaultOrientation,
										int pageMarginVertical = PageData.k_DefaultMargin,
										int pageMarginHorizontal = PageData.k_DefaultMargin)
		{
			var page = new PageData();
			page.SetSize(pageSize).SetOrientation(pageOrientation).SetMargins(pageMarginVertical, pageMarginHorizontal);
			return new PDFJObjectFactory(page);
		}

		private PDFJObjectFactory(PageData page)
		{
			page._parent = this;
			_page = page;
			_datas = new List<BasePDFData>();
		}

		#region AddText
		public PDFTextData AddText()
		{
			var data = new PDFTextData(this);
			_datas.Add(data);
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

		//public PDFImageData AddImage(string url, Rect rect)
		//{
		//	return AddImage(url).SetPosition(rect.x, rect.y).SetSize(rect.width, rect.height);
		//}

		//public PDFImageData AddImage(Rect rect)
		//{
		//	return AddImage().SetPosition(rect.x, rect.y).SetSize(rect.width, rect.height);
		//}

		//public PDFImageData AddImage(float x, float y)
		//{
		//	return AddImage().SetPosition(x, y);
		//}

		public PDFImageData AddImage()
		{
			var data = new PDFImageData(this);
			_datas.Add(data);
			return data;
		}
		#endregion

		#region AddList
		public PDFListData AddList()
		{
			var data = new PDFListData(this);
			_datas.Add(data);
			return data;
		}
		#endregion

		#region AddTable
		public PDFTableData AddTable()
		{
			var data = new PDFTableData(this);
			_datas.Add(data);
			return data;
		}
		#endregion

		public JObject Create()
		{
			#region Backup
			//(JObject page, JObject[] content) result = new();
			//result.page = m_Page.GetExportData();
			//result.content = new JObject[m_Datas.Count];
			//for (int i = 0; i < m_Datas.Count; i++)
			//{
			//	result.content[i] = JObject.FromObject(m_Datas[i]);
			//}
			//return result;
			#endregion

			var page = _page.GetExportData();
			var content = new JArray();
			foreach (var item in _datas)
				content.Add(item.GetExportData());
			return new JObject(
				new JProperty("page", page),
				new JProperty("content", content));
		}
	}
}
