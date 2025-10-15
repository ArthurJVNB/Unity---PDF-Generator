using Newtonsoft.Json;
using NUnit.Framework;

namespace Project.PDFGenerator.Tests
{
	public class PDFPageDataTest
	{
		[Test]
		public void TestJson()
		{
			const string expected = "{\"size\":\"A4\",\"orientation\":\"portrait\",\"margins\":{\"top\":40,\"right\":40,\"bottom\":40,\"left\":40}}";
			string actual = new PageData().SetSize(PageSize.A4).SetOrientation(PageOrientation.Portrait).SetMargins(40).GetExportData().ToString(Formatting.None);
			Assert.AreEqual(expected, actual, "Expected: {0}\n  Received: {1}", expected, actual);
		}
	}
}
