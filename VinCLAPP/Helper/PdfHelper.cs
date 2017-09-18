using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace VinCLAPP.Helper
{
    public class PdfHelper
    {
        public MemoryStream WritePdf(string bodyPdf)
        {
            var workStream = new MemoryStream();
            var reader = new StringReader(bodyPdf);
            var document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            var worker = new HTMLWorker(document);
            document.Open();
            worker.StartDocument();
            worker.Parse(reader);
            worker.EndDocument();
            document.Close();
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            return workStream;
        }
    }
}
