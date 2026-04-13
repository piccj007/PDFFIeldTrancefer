using System;
using System.Collections.Generic;
using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Geom;

public class PdfFieldSizeExtractor
{
    public static Dictionary<string, (float w, float h)> Extract(string oldPdfPath)
    {
        var sizeMap = new Dictionary<string, (float w, float h)>();

        using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(oldPdfPath)))
        {
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, false);
            var fields = form.GetFormFields();

            Console.WriteLine("\n===== EXTRACTING FIELD SIZES =====\n");

            foreach (var field in fields)
            {
                string name = field.Key;
                var widgets = field.Value.GetWidgets();

                if (widgets != null && widgets.Count > 0)
                {
                    Rectangle rect = widgets[0].GetRectangle().ToRectangle();

                    float w = rect.GetWidth();
                    float h = rect.GetHeight();

                    sizeMap[name] = (w, h);

                    Console.WriteLine($"SIZE → {name}: W={w}, H={h}");
                }
            }
        }

        return sizeMap;
    }
}