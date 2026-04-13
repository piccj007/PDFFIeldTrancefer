using System;

public class PdfFieldAnalyzer
{
    private readonly PdfReaderService _reader = new();
    private readonly PdfFieldService _fieldService = new();
    private readonly ConsoleLogger _logger = new();

    public void Analyze(string filePath)
    {
        _logger.Header(filePath);

        var fields = _reader.GetFields(filePath);

        if (fields == null)
        {
            _logger.Error("AcroForm not found");
            return;
        }

        if (fields.Count == 0)
        {
            _logger.Error("No fields found");
            return;
        }

        _logger.Success($"Total Fields: {fields.Count}");
        Console.WriteLine("----------------------------------------");

        int index = 1;

        foreach (var f in fields)
        {
            string type = _fieldService.GetFieldType(f.Value);

            Console.WriteLine($"{index}. {f.Key} ({type})");
            index++;
        }

        _logger.Footer();
    }
}