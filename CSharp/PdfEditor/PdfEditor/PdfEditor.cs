using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Linq;

namespace PdfEditor
{
    /// <summary>
    /// This class provides a functionality to work with PDF files
    /// This class uses iTextSharp dll
    /// </summary>
    class PdfEditor
    {
        PdfReader reader;
        public PdfEditor(string inputPdf)
        {
            reader = new PdfReader(inputPdf);
        }
        public void TrimLastPages(int count)
        {
            reader.SelectPages(String.Format("1-{0}",reader.NumberOfPages-count));
        }
        public void Save(string outputPdf)
        {
            using (PdfStamper stamper = new PdfStamper(reader, File.Create(outputPdf)))
            {
                stamper.Close();
            }
        }
        /// <summary>
        /// Converts a Word file to PDF 
        /// </summary>
        /// <param name="inputFile">The path of the input file</param>
        /// <param name="outputFile">The path of the output file</param>
        public static void ConvertWordToPdf(string inputFile, string outputFile)
        {
            //Read the Data from Input File
            StreamReader rdr = new StreamReader(inputFile);
            //Create a New instance on Document Class
            Document doc = new Document();
            //Create a New instance of PDFWriter Class for Output File
            PdfWriter.GetInstance(doc, new FileStream(outputFile, FileMode.Create));
            //Open the Document
            doc.Open();
            //Add the content of Text File to PDF File
            doc.Add(new Paragraph(rdr.ReadToEnd()));
            //Close the Document
            doc.Close();

            ////Open the Converted PDF File
            //System.Diagnostics.Process.Start(txtOutput.Text);
        }
        /// <summary>
        /// Copies PDF files from the input folder to destination folder
        /// </summary>
        /// <param name="inputFolder">The path of the input folder</param>
        /// <param name="outputFolder">The path of the output folder</param>
        public static void CopyPdfs(string inputFolder, string outputFolder)
        {
            var files = Directory.GetFiles(inputFolder);
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Split('.').Last() == "pdf")
                {
                    File.Copy(files[i], outputFolder + files[i].Split('\\').Last());
                }
            }
            var dirs = Directory.GetDirectories(inputFolder);
            for (int i = 0; i < dirs.Length; i++)
            {
                var dirName = dirs[i].Split('\\').Last();
                if (!Directory.Exists(outputFolder + dirName)) Directory.CreateDirectory(outputFolder + dirName);
                PdfEditor.CopyPdfs(dirs[i], outputFolder + dirName + "\\");
            }
        }
    }
}
