using System.Collections.Generic;
using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;

public class PdfReaderService
{
    public IDictionary<string, PdfFormField> GetFields(string filePath)
    {
        using var pdf = new PdfDocument(new PdfReader(filePath));
        var form = PdfAcroForm.GetAcroForm(pdf, false);

        if (form == null)
            return null;

        return form.GetFormFields(); // ✅ no issue now
    }
}