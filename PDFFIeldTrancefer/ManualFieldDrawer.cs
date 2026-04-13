using System;
using System.Collections.Generic;
using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Geom;

public class ManualFieldDrawer
{
    public static void DrawFields(string pdfPath, string outputPath)
    {
        using (PdfReader reader = new PdfReader(pdfPath))
        using (PdfWriter writer = new PdfWriter(outputPath))
        using (PdfDocument pdfDoc = new PdfDocument(reader, writer))
        {
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

            PdfPage page = pdfDoc.GetFirstPage();

            // 🔥 Manual field positions (you control everything)
            var fields = new Dictionary<string, Rectangle>
            {
                { "Name", new Rectangle(100, 700, 250, 20) },
                { "EntID", new Rectangle(380, 700, 200, 20) },
                { "Text1", new Rectangle(100, 650, 250, 20) },

                { "CheckBox1", new Rectangle(500, 500, 15, 15) },
                { "RadioButton1", new Rectangle(520, 500, 15, 15) },

                { "ComboBox1", new Rectangle(300, 600, 200, 20) },
                { "Button1", new Rectangle(250, 720, 100, 20) }
            };

            foreach (var field in fields)
            {
                string name = field.Key;
                Rectangle rect = field.Value;

                PdfFormField newField;

                if (name.Contains("CheckBox") || name.Contains("Radio"))
                {
                    newField = PdfButtonFormField.CreateCheckBox(pdfDoc, rect, name, "Yes");
                }
                else if (name.Contains("Combo"))
                {
                    newField = PdfChoiceFormField.CreateComboBox(pdfDoc, rect, name, "", new string[] { });
                }
                else if (name.Contains("Button"))
                {
                    newField = PdfButtonFormField.CreatePushButton(pdfDoc, rect, name, "Click");
                }
                else
                {
                    newField = PdfTextFormField.CreateText(pdfDoc, rect, name, "");
                }

                form.AddField(newField, page);

                Console.WriteLine($"✔ Drawn field: {name}");
            }

            Console.WriteLine("✅ Manual fields created successfully!");
        }
    }
}