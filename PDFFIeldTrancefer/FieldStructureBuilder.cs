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

            var oldFields = oldForm.GetFormFields();
            PdfPage page = newPdf.GetFirstPage();

            foreach (var field in oldFields)
            {
                string name = field.Key;
                PdfFormField oldField = field.Value;

                var widgets = oldField.GetWidgets();

                float w = 100;
                float h = 20;

                if (widgets != null && widgets.Count > 0)
                {
                    Rectangle rect = widgets[0].GetRectangle().ToRectangle();

                    // 🔥 ONLY SIZE USE
                    w = rect.GetWidth();
                    h = rect.GetHeight();

                    Console.WriteLine($"SIZE → {name}: W={w}, H={h}");
                }

                // 🔥 SAME POSITION FOR ALL (ignore position logic)
                float x = 50;
                float y = 700;

                Rectangle newRect = new Rectangle(x, y, w, h);

                PdfFormField newField;

                if (oldField is PdfTextFormField)
                {
                    newField = PdfTextFormField.CreateText(newPdf, newRect, name, "");
                }
                else if (oldField is PdfButtonFormField)
                {
                    newField = PdfButtonFormField.CreateCheckBox(newPdf, newRect, name, "Yes");
                }
                else if (oldField is PdfChoiceFormField)
                {
                    newField = PdfChoiceFormField.CreateComboBox(newPdf, newRect, name, "", new string[] { });
                }
                else
                {
                    newField = PdfTextFormField.CreateText(newPdf, newRect, name, "");
                }

                newForm.AddField(newField, page);
            }

            Console.WriteLine("✅ Field structure created (no layout)");
        }
    }
}