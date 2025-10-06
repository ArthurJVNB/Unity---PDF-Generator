using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

namespace Project.PDFGenerator
{
    public class PdfTest : MonoBehaviour
    {
        private const string _api = "https://www.propixelgames.online/";
        private const string _post_create_pdf = _api + "pdf/createpdf.php";
        public void GerarPDF()
        {
            StartCoroutine(GerarPDFCoroutine());
        }
        private JObject JObjectPdf
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
        IEnumerator GerarPDFCoroutine()
        {
            string jsonString = JObjectPdf.ToString();
            WWWForm form = new WWWForm();
            form.AddField("json", jsonString);
            string url = _post_create_pdf;
            using (UnityWebRequest request = UnityWebRequest.Post(url, form))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Erro: " + request.error);
                }
                else
                {
                    //pra remove BOM UTF-8 se existir
                    string pdf_url = request.downloadHandler.text.TrimStart('\uFEFF').Trim();
                    Debug.Log(pdf_url);
                    Application.OpenURL(pdf_url);
                }
            }
        }
    }
}
