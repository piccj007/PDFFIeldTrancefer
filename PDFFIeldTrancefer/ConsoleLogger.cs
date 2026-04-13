using System;

public class ConsoleLogger
{
    public void Header(string file)
    {
        Console.WriteLine("\n========================================");
        Console.WriteLine("📄 PDF ANALYSIS START");
        Console.WriteLine($"File: {file}");
        Console.WriteLine("========================================");
    }

    public void Footer()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("✅ ANALYSIS COMPLETE");
        Console.WriteLine("========================================\n");
    }

    public void Error(string msg)
    {
        Console.WriteLine($"❌ ERROR: {msg}");
    }

    public void Success(string msg)
    {
        Console.WriteLine($"✅ {msg}");
    }
}
