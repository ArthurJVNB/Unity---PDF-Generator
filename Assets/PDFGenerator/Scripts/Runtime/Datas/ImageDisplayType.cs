namespace Project.PDFGenerator
{
	public enum ImageDisplayType
	{
		/// <summary>
		/// Fica na mesma linha que outros elementos
		/// </summary>
		Block,

		/// <summary>
		/// Ocupa toda a linha e força quebra antes/depois
		/// </summary>
		Inline,

		/// <summary>
		/// Fica na linha, mas aceita largura e altura
		/// </summary>
		InlineBlock,

		/// <summary>
		/// Oculta o elemento, ou seja, não aparece no PDF
		/// </summary>
		None,
	}
}
