using iText.Forms.Fields;
using iText.Kernel.Pdf;

public class PdfFieldService
{
    public string GetFieldType(PdfFormField field)
    {
        var ft = field.GetFormType();

        if (ft == PdfName.Tx) return "TextBox";

        if (ft == PdfName.Btn)
        {
            if (field is PdfButtonFormField btn)
            {
                if (btn.IsPushButton()) return "Button";
                if (btn.IsRadio()) return "RadioButton";
                return "CheckBox";
            }
            return "Button";
        }

        if (ft == PdfName.Ch) return "Dropdown/List";

        return "Unknown";
    }
}