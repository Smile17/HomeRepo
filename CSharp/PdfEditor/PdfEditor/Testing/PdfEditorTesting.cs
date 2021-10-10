namespace PdfEditor.Testing
{
    class PdfEditorTesting
    {
        public static void TrimLastPages()
        {
            PdfEditor pdf = new PdfEditor("input.pdf");
            pdf.TrimLastPages(1);
            pdf.Save("output.pdf");
        }
        public static void CopyPdfs()
        {
            PdfEditor.CopyPdfs(@"C:\Users\1\OneDrive\", @"D:\TMP\");
        }
    }
}
