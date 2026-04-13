using System;
using System.Collections.Generic;
using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Geom;

public class DebugFieldLayout
{
    public static void GenerateDebugPdf(string oldPdfPath, string outputPdfPath)
    {
        var sizeMap = PdfFieldSizeExtractor.Extract(oldPdfPath);

        using (PdfDocument oldPdf = new PdfDocument(new PdfReader(oldPdfPath)))
        using (PdfDocument newPdf = new PdfDocument(new PdfWriter(outputPdfPath)))
        {
            PdfAcroForm oldForm = PdfAcroForm.GetAcroForm(oldPdf, false);
            PdfAcroForm newForm = PdfAcroForm.GetAcroForm(newPdf, true);

            var fields = oldForm.GetFormFields();

            PdfPage page = newPdf.AddNewPage();

            float startX = 50;
            float startY = 750;
            float gap = 50;

            float currentY = startY;

            foreach (var field in fields)
            {
                string name = field.Key;
                PdfFormField oldField = field.Value;

                float w = 100;
                float h = 20;

                if (sizeMap.ContainsKey(name))
                {
                    (w, h) = sizeMap[name];
                }

                // 🔥 NEW PAGE LOGIC
                if (currentY < 50)
                {
                    page = newPdf.AddNewPage();
                    currentY = startY;
                }

                Rectangle rect = new Rectangle(startX, currentY, w, h);

                PdfFormField newField;

                if (oldField is PdfTextFormField)
                {
                    newField = PdfTextFormField.CreateText(newPdf, rect, name, "");
                }
                else if (oldField is PdfButtonFormField)
                {
                    newField = PdfButtonFormField.CreateCheckBox(newPdf, rect, name, "Yes");
                }
                else if (oldField is PdfChoiceFormField)
                {
                    newField = PdfChoiceFormField.CreateComboBox(newPdf, rect, name, "", new string[] { });
                }
                else
                {
                    newField = PdfTextFormField.CreateText(newPdf, rect, name, "");
                }

                newForm.AddField(newField, page);

                Console.WriteLine($"DEBUG → {name} at Y={currentY}");

                currentY -= gap; // 🔥 50pt shift
            }

            Console.WriteLine("✅ Debug PDF generated");
        }
    }
}