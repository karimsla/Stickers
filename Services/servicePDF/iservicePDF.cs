using iTextSharp.text.pdf;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface iservicePDF
    {
        PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, Command cmd);
         void AddCellToBody(PdfPTable tableLayout, string cellText);
        void AddCellToHeader(PdfPTable tableLayout, string cellText);
    }
}
