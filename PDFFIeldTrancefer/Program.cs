using System;

class Program
{
    static void Main()
    {
        string oldPdfPath = @"D:\CSProject\Forms - Copy\old pdf.pdf";
        string newPdfPath = @"D:\CSProject\Forms - Copy\new pdf.pdf";

        Console.WriteLine("===== OLD PDF ANALYSIS =====");
        var analyzer = new PdfFieldAnalyzer();
        analyzer.Analyze(oldPdfPath);

        Console.WriteLine("\n\n===== NEW PDF CHECK =====");
        NewPdfDetector.Check(newPdfPath); // ✅ FIXED

        Console.ReadKey();
    }
}