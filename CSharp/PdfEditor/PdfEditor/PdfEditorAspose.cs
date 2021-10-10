using Aspose.Pdf.Devices;
using System;
using System.IO;

namespace PdfEditor
{
    class PdfEditorAspose
    {
        public static void ParsePdfToImage(string name)
        {
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(name);
            ParsePdfToImage(name, 1, pdfDocument.Pages.Count);

        }
        public static void ParsePdfToImage(string name, int Min, int Max)
        {
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(name);

            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);
            string str = name + "_" + Min.ToString() + "_" + Max.ToString();
            Directory.CreateDirectory(str);
            Directory.SetCurrentDirectory(str);

            for (int i = Min; i <= Math.Min(Max, pdfDocument.Pages.Count); i++)
                pngDevice.Process(pdfDocument.Pages[i], i + ".png");

        }
    }
}
