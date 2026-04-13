using System;
using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Geom;

public class PdfFieldDebugger
{
    public static void CompareFieldAngles(string oldPdfPath, string newPdfPath)
    {
        using (PdfDocument oldPdf = new PdfDocument(new PdfReader(oldPdfPath)))
        using (PdfDocument newPdf = new PdfDocument(new PdfReader(newPdfPath)))
        {
            var oldForm = PdfAcroForm.GetAcroForm(oldPdf, false);
            var newForm = PdfAcroForm.GetAcroForm(newPdf, false);

            var oldFields = oldForm.GetFormFields();
            var newFields = newForm.GetFormFields();

            Console.WriteLine("\n===== FIELD ANGLE DEBUG =====\n");

            foreach (var field in oldFields)
            {
                string name = field.Key;

                if (!newFields.ContainsKey(name))
                {
                    Console.WriteLine($"❌ Missing in NEW: {name}");
                    continue;
                }

                var oldWidget = field.Value.GetWidgets()[0];
                var newWidget = newFields[name].GetWidgets()[0];

                Rectangle oldRect = oldWidget.GetRectangle().ToRectangle();
                Rectangle newRect = newWidget.GetRectangle().ToRectangle();

                string oldOrientation = GetOrientation(oldRect);
                string newOrientation = GetOrientation(newRect);

                if (oldOrientation != newOrientation)
                {
                    Console.WriteLine($"⚠️ MISMATCH: {name}");
                    Console.WriteLine($"   OLD: {oldOrientation} (W:{oldRect.GetWidth()}, H:{oldRect.GetHeight()})");
                    Console.WriteLine($"   NEW: {newOrientation} (W:{newRect.GetWidth()}, H:{newRect.GetHeight()})");
                }
                else
                {
                    Console.WriteLine($"✅ OK: {name} ({oldOrientation})");
                }
            }

            Console.WriteLine("\n===== DEBUG END =====");
        }
    }

    private static string GetOrientation(Rectangle rect)
    {
        float w = rect.GetWidth();
        float h = rect.GetHeight();

        if (h > w * 2)
            return "VERTICAL (ROTATED)";
        else if (w > h * 2)
            return "HORIZONTAL";
        else
            return "NORMAL";
    }
}