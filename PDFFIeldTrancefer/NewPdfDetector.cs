using System;
using iText.Kernel.Pdf;
using iText.Forms;

public class NewPdfDetector
{
    public static void Check(string pdfPath)
    {
        Console.WriteLine("===================================");
        Console.WriteLine("NEW PDF CHECK START");
        Console.WriteLine($"File: {pdfPath}");
        Console.WriteLine("===================================");

        using (PdfReader reader = new PdfReader(pdfPath))
        using (PdfDocument pdfDoc = new PdfDocument(reader))
        {
            Console.WriteLine($"Total Pages: {pdfDoc.GetNumberOfPages()}");

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, false);

            if (form == null || form.GetFormFields().Count == 0)
            {
                Console.WriteLine("✅ Blank PDF (No form fields found)");
            }
            else
            {
                Console.WriteLine("⚠️ PDF contains form fields");
            }
        }

        Console.WriteLine("===================================");
        Console.WriteLine("CHECK COMPLETE");
        Console.WriteLine("===================================");
    }
}