using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotepadDesktop.models;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace NotepadDesktop.utils
{
    public class PdfExporter
    {
        [Obsolete]
        public static void ExportNoteToPdf(Note note, string filePath)
        {
            Document document = new Document();
            Section section = document.AddSection();

            Paragraph title = section.AddParagraph();
            title.Format.Font.Size = 15;
            title.Format.Font.Bold = true;
            title.AddText(note.Title);

            Paragraph date = section.AddParagraph();
            date.Format.Font.Size = 12;
            date.AddText("\nData utworzenia: " + note.CreatingDate.ToString("g"));

            Paragraph content = section.AddParagraph();
            content.Format.Font.Size = 12;
            content.AddText("\n" + note.Content);

            
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true) { Document = document };

            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(filePath);
        }
    }
}
