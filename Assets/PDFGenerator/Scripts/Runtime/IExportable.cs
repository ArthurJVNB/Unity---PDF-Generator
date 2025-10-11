namespace Project.PDFGenerator
{
	public interface IExportable<T>
	{
		public T GetExportData();
	}
}
