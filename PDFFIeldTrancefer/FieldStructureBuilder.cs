using System;
using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Geom;

public class FieldStructureBuilder
{
    public static void Build(string oldPdfPath, string newPdfPath, string outputPdfPath)
    {
        using (PdfDocument oldPdf = new PdfDocument(new PdfReader(oldPdfPath)))
        using (PdfDocument newPdf = new PdfDocument(new PdfReader(newPdfPath), new PdfWriter(outputPdfPath)))
        {
            PdfAcroForm oldForm = PdfAcroForm.GetAcroForm(oldPdf, false);
            PdfAcroForm newForm = PdfAcroForm.GetAcroForm(newPdf, true);
            var sizeMap = PdfFieldSizeExtractor.Extract(oldPdfPath);
            var oldFields = oldForm.GetFormFields();
            PdfPage page = newPdf.GetFirstPage();

            foreach (var field in oldFields)
            {
                string name = field.Key;
                PdfFormField oldField = field.Value;

                // 🔥 Dummy rect (same place for all)
                //   Rectangle dummyRect = new Rectangle(10, 10, 10, 10);
                float w = 10;
                float h = 10;

                if (sizeMap.ContainsKey(name))
                {
                    (w, h) = sizeMap[name];
                }

                Rectangle dummyRect = new Rectangle(10, 10, h, w);

                PdfFormField newField;

                if (oldField is PdfTextFormField)
                {
                    newField = PdfTextFormField.CreateText(newPdf, dummyRect, name, "");
                    Console.WriteLine($"TEXT → {name}");
                }
                else if (oldField is PdfButtonFormField)
                {
                    newField = PdfButtonFormField.CreateCheckBox(newPdf, dummyRect, name, "Yes");
                    Console.WriteLine($"BUTTON → {name}");
                }
                else if (oldField is PdfChoiceFormField)
                {
                    newField = PdfChoiceFormField.CreateComboBox(newPdf, dummyRect, name, "", new string[] { });
                    Console.WriteLine($"DROPDOWN → {name}");
                }
                else
                {
                    newField = PdfTextFormField.CreateText(newPdf, dummyRect, name, "");
                    Console.WriteLine($"UNKNOWN → {name}");
                }

                newForm.AddField(newField, page);
            }

            Console.WriteLine("✅ Field structure created (no layout)");
        }
    }
}