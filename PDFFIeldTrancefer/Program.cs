using System;

class Program
{
    static void Main()
    {
        string oldPdf = @"D:\CSProject\Forms - Copy\old pdf.pdf";
        string newPdf = @"D:\CSProject\Forms - Copy\new pdf.pdf";

        string outputPdf = $@"D:\CSProject\Forms - Copy\output_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
        PdfFieldSizeExtractor.Extract(oldPdf);
        FieldStructureBuilder.Build(oldPdf, newPdf, outputPdf);

        // 🔥 Step 1: Copy fields
       // PdfFieldCopier.CopyFields(oldPdf, newPdf, outputPdf);

       // // 🔥 Step 2: Compare OLD vs OUTPUT
       //PdfFieldDebugger.CompareFieldAngles(oldPdf, outputPdf);


       // string inputPdf = @"D:\CSProject\Forms - Copy\new pdf.pdf";

       // string outputPdf1 = $@"D:\CSProject\Forms - Copy\manual_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

       // ManualFieldDrawer.DrawFields(inputPdf, outputPdf1);

       

        Console.ReadKey();
    }
}