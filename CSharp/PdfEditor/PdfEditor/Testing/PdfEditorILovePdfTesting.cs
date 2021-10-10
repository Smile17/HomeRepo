using LovePdf.Core;

namespace PdfEditor.Testing
{
    class PdfEditorILovePdfTesting
    {
        PdfEditorILovePdf editor;
        public PdfEditorILovePdfTesting()
        {
            var api = new LovePdfApi("project_public_d05a34830b9ffe834d2caa383e658c60_q7hibc733aac64687a94c7739196a9e6820a5",
                "secret_key_9b041910b78801c19395c80aaee5376b_rigqe39d83080a5fd5cca9e01aabe9c92ab61");
            editor = new PdfEditorILovePdf(api);
        }
       
        public void CompressFile()
        {
            editor.CompressFile(@"D:\Smile\Documents\Visual Studio 2015\Projects\PdfEditor\PdfEditor\bin\Debug",
                @"D:\Smile\Documents\Visual Studio 2015\Projects\PdfEditor\PdfEditor\bin\Debug\input.pdf");
        }
        public void CompressFolder()
        {
            editor.CompressFolder(@"C:\Users\1\OneDrive\Programming");
        }
    }
}
