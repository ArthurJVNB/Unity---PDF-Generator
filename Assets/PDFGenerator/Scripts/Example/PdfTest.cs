using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class PdfTest : MonoBehaviour
{
    private const string _api = "https://www.propixelgames.online/";
    private const string _post_create_pdf = _api + "pdf/createpdf.php";

    public void GerarPDF()
    {
        StartCoroutine(GerarPDFCoroutine());
    }

    #region Example 1
    private JObject JObjectPdf
    {
        get
        {
            return new JObject(
                new JProperty("page", new JObject(
                    new JProperty("size", "A4"),
                    new JProperty("orientation", "portrait"),
                    new JProperty("margins", new JObject(
                        new JProperty("top", 40),
                        new JProperty("right", 40),
                        new JProperty("bottom", 40),
                        new JProperty("left", 40)
                    ))
                )),
                new JProperty("content", new JArray(
                    // Título
                    new JObject(
                        new JProperty("type", "title"),
                        new JProperty("text", "Relatório de Vendas - Outubro 2025"),
                        new JProperty("style", new JObject(
                            new JProperty("text-align", "center"),
                            new JProperty("color", "#2E86C1"),
                            new JProperty("font-size", "26px"),
                            new JProperty("margin-bottom", "25px"),
                            new JProperty("font-weight", "bold")
                        ))
                    ),

                    // Parágrafo
                    new JObject(
                        new JProperty("type", "paragraph"),
                        new JProperty("text", "Este relatório foi gerado automaticamente via Unity + PHP, demonstrando a integração entre as plataformas para gerar documentos PDF dinâmicos."),
                        new JProperty("style", new JObject(
                            new JProperty("font-size", "14px"),
                            new JProperty("margin-bottom", "15px"),
                            new JProperty("text-align", "justify"),
                            new JProperty("line-height", "1.4")
                        ))
                    ),

                    // Imagem
                    new JObject(
                        new JProperty("type", "image"),
                        new JProperty("src", "http://www.propixelgames.online/pdf/images/icon.png"),
                        new JProperty("style", new JObject(
                            new JProperty("display", "block"),
                            new JProperty("margin", "10px auto"),
                            new JProperty("width", "120px"),
                            new JProperty("height", "120px")
                        ))
                    ),

                    // Subtítulo
                    new JObject(
                        new JProperty("type", "subtitle"),
                        new JProperty("text", "Resumo Geral"),
                        new JProperty("style", new JObject(
                            new JProperty("font-size", "18px"),
                            new JProperty("color", "#1B4F72"),
                            new JProperty("margin-top", "25px"),
                            new JProperty("margin-bottom", "10px"),
                            new JProperty("font-weight", "bold")
                        ))
                    ),

                    // Lista
                    new JObject(
                        new JProperty("type", "list"),
                        new JProperty("ordered", false),
                        new JProperty("items", new JArray(
                            "Período: 01 a 30 de Outubro de 2025",
                            "Total de pedidos: 34",
                            "Clientes atendidos: 29",
                            "Lucro bruto: R$ 47.500,00"
                        )),
                        new JProperty("style", new JObject(
                            new JProperty("font-size", "13px"),
                            new JProperty("margin-bottom", "20px")
                        ))
                    ),

                    // Tabela
                    new JObject(
                        new JProperty("type", "table"),
                        new JProperty("header", new JArray("Produto", "Quantidade", "Preço Unitário", "Total")),
                        new JProperty("rows", new JArray(
                            new JArray("Nintendo Switch", "10", "R$2.000", "R$20.000"),
                            new JArray("Smash Bros Ultimate", "5", "R$400", "R$2.000"),
                            new JArray("Pro Controller", "8", "R$300", "R$2.400"),
                            new JArray("Zelda Tears of the Kingdom", "6", "R$450", "R$2.700"),
                            new JArray("Amiibo Mario", "12", "R$180", "R$2.160")
                        )),
                        new JProperty("style", new JObject(
                            new JProperty("width", "100%"),
                            new JProperty("border", "1px solid #000"),
                            new JProperty("border-collapse", "collapse"),
                            new JProperty("font-size", "12px"),
                            new JProperty("margin-bottom", "20px")
                        )),
                        new JProperty("headerStyle", new JObject(
                            new JProperty("background-color", "#2E86C1"),
                            new JProperty("color", "#FFFFFF"),
                            new JProperty("font-weight", "bold")
                        )),
                        new JProperty("rowStyle", new JArray(
                            new JObject(new JProperty("background-color", "#f9f9f9")),
                            new JObject(new JProperty("background-color", "#ffffff"))
                        ))
                    ),

                    // Parágrafo final
                    new JObject(
                        new JProperty("type", "paragraph"),
                        new JProperty("text", "Os dados acima representam apenas uma simulação para testes de geração de PDF. Todos os valores são fictícios e destinados a fins de demonstração."),
                        new JProperty("style", new JObject(
                            new JProperty("font-size", "12px"),
                            new JProperty("font-style", "italic"),
                            new JProperty("text-align", "justify"),
                            new JProperty("margin-top", "15px")
                        ))
                    ),

                    // Rodapé
                    new JObject(
                        new JProperty("type", "footer"),
                        new JProperty("text", "Propixel Games © 2025 — Relatórios automatizados via Unity"),
                        new JProperty("style", new JObject(
                            new JProperty("text-align", "center"),
                            new JProperty("font-size", "10px"),
                            new JProperty("color", "#888"),
                            new JProperty("margin-top", "40px")
                        ))
                    )
                ))
            );
        }
    }
    #endregion

    #region Example 2
    private JObject JObjectPdf2
    {
        get
        {
            return new JObject(
                new JProperty("page", new JObject(
                    new JProperty("size", "A4"),
                    new JProperty("orientation", "portrait")
                )),
                new JProperty("content", new JArray(
                    new JObject(
                        new JProperty("type", "title"),
                        new JProperty("text", "Relatório de Vendas"),
                        new JProperty("style", new JObject(
                            new JProperty("text-align", "center"),
                            new JProperty("color", "#2E86C1"),
                            new JProperty("font-size", "24px")
                        ))
                    ),
                    new JObject(
                        new JProperty("type", "paragraph"),
                        new JProperty("text", "Teste Gerando via Unity + PHP"),
                        new JProperty("style", new JObject(
                            new JProperty("font-size", "14px"),
                            new JProperty("margin-bottom", "10px")
                        ))
                    ),
                    new JObject(
                        new JProperty("type", "image"),
                        new JProperty("src", "http://www.propixelgames.online/pdf/images/icon.png"),
                        new JProperty("style", new JObject(
                            new JProperty("display", "block"),
                            new JProperty("margin", "10px auto")
                        ))
                    ),
                    new JObject(
                        new JProperty("type", "table"),
                        new JProperty("header", new JArray("Produto", "Quantidade", "Preço")),
                        new JProperty("rows", new JArray(
                            new JArray("Nintendo Switch", "10", "R$2000"),
                            new JArray("Smash bross ultimate", "5", "R$400"),
                            new JArray("Pro Controller", "8", "R$300")
                        )),
                        new JProperty("style", new JObject(
                            new JProperty("width", "100%"),
                            new JProperty("border", "1px solid #000")
                        ))
                    )
                ))
            );
        }
    }
    #endregion

    private IEnumerator GerarPDFCoroutine()
    {
        string jsonString = JObjectPdf.ToString();
        WWWForm form = new WWWForm();
        form.AddField("json", jsonString);

        using (UnityWebRequest request = UnityWebRequest.Post(_post_create_pdf, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao gerar PDF: " + request.error);
            }
            else
            {
                string pdfUrl = request.downloadHandler.text.TrimStart('\uFEFF').Trim();
                Debug.Log("PDF URL: " + pdfUrl);
                Application.OpenURL(pdfUrl);
            }
        }
    }
}
