#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Project.PDFGenerator.Editor
{
    [CustomEditor(typeof(PdfTest))]
    public class PdfTestEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Mostra o Inspector normal

            PdfTest pdfTest = (PdfTest)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Gerar PDF"))
            {
                pdfTest.GerarPDF();
            }
        }
    }
}
#endif