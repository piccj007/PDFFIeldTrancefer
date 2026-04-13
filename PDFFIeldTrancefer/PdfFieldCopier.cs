using System;
using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Geom;

public class PdfFieldCopier
{
    public static void CopyFields(string oldPdfPath, string newPdfPath, string outputPdfPath)
    {
        using (PdfReader oldReader = new PdfReader(oldPdfPath))
        using (PdfDocument oldPdf = new PdfDocument(oldReader))

        using (PdfReader newReader = new PdfReader(newPdfPath))
        using (PdfWriter writer = new PdfWriter(outputPdfPath))
        using (PdfDocument newPdf = new PdfDocument(newReader, writer))
        {
            PdfAcroForm oldForm = PdfAcroForm.GetAcroForm(oldPdf, false);
            PdfAcroForm newForm = PdfAcroForm.GetAcroForm(newPdf, true);

            var oldFields = oldForm.GetFormFields();

            foreach (var field in oldFields)
            {
                string name = field.Key;
                PdfFormField oldField = field.Value;

                var widgets = oldField.GetWidgets();

                foreach (var widget in widgets)
                {
                    int pageNum = oldPdf.GetPageNumber(widget.GetPage());

                    PdfPage oldPage = oldPdf.GetPage(pageNum);
                    PdfPage newPage = newPdf.GetPage(pageNum);

                    Rectangle rect = widget.GetRectangle().ToRectangle();

                    float x = rect.GetX();
                    float y = rect.GetY();
                    float w = rect.GetWidth();
                    float h = rect.GetHeight();

                    // 🔥 MAIN FIX (Y flip)
                    float pageHeight = newPage.GetPageSize().GetHeight();
                    float correctedY = pageHeight - y - h;

                    Rectangle newRect = new Rectangle(x, correctedY, w, h);

                    // 🔥 Field creation (safe)
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

                    newField.SetPage(pageNum);
                    newForm.AddField(newField, newPage);

                    Console.WriteLine($"✔ Copied field: {name}");
                }
            }

            Console.WriteLine("✅ All fields copied successfully!");
        }
    }
}