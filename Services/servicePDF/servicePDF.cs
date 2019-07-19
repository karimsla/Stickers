using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Model;

namespace Services
{
    public class servicePDF : iservicePDF
    {


        public PdfPTable pdf_table(PdfPTable tableLayout,List<Command> lscmd)
        {
            float[] headers = { 30, 30, 30, 30, 30, 30, 30, 30, 30, 30 }; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            //Add Title to the PDF file at the top  





            tableLayout.AddCell(new PdfPCell(new Phrase("the details of the command", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0))))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });


            ////Add header  
            AddCellToHeader(tableLayout, "IdCommand");
            AddCellToHeader(tableLayout, "Product");
            AddCellToHeader(tableLayout, "Quantity");
            AddCellToHeader(tableLayout, "Name");
            AddCellToHeader(tableLayout, "Phone");
            AddCellToHeader(tableLayout, "Phone 2");
            AddCellToHeader(tableLayout, "Adresse");
            AddCellToHeader(tableLayout, "City");
            AddCellToHeader(tableLayout, "Zip Code");
            AddCellToHeader(tableLayout, "Email");
            ////Add body  
            IserviceProduct spp = new serviceProduct();
            foreach (var cmd in lscmd)
            {
                AddCellToBody(tableLayout, cmd.idcmd.ToString());
                AddCellToBody(tableLayout, spp.GetById(cmd.idprod).nameprod);
                AddCellToBody(tableLayout, cmd.qteprod.ToString());
                AddCellToBody(tableLayout, cmd.name.ToString());
                AddCellToBody(tableLayout, cmd.phone.ToString());
                AddCellToBody(tableLayout, cmd.phone2.ToString());
                AddCellToBody(tableLayout, cmd.adresse);
                AddCellToBody(tableLayout, cmd.gov);
                AddCellToBody(tableLayout, cmd.code);
                AddCellToBody(tableLayout, cmd.email);
            }

            return tableLayout;

        }

        public PdfPTable Add_Content_To_PDF(PdfPTable tableLayout,Command cmd)
        {

            float[] headers = { 30, 30, 30, 30, 30, 30, 30, 30, 30, 30}; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            //Add Title to the PDF file at the top  

            



            tableLayout.AddCell(new PdfPCell(new Phrase("the details of the command", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0)))) {
                Colspan = 12, Border = 0, PaddingBottom = 5, HorizontalAlignment = Element.ALIGN_CENTER
                    });


            ////Add header  
            AddCellToHeader(tableLayout,"IdCommand");
            AddCellToHeader(tableLayout, "Product");
            AddCellToHeader(tableLayout, "Quantity");
            AddCellToHeader(tableLayout, "Name");
            AddCellToHeader(tableLayout, "Phone");
            AddCellToHeader(tableLayout, "Phone 2");
            AddCellToHeader(tableLayout, "Adresse");
            AddCellToHeader(tableLayout, "City");
            AddCellToHeader(tableLayout, "Zip Code");
            AddCellToHeader(tableLayout, "Email");
            ////Add body  
            IserviceProduct spp = new serviceProduct();
            AddCellToBody(tableLayout, cmd.idcmd.ToString());
            AddCellToBody(tableLayout, spp.GetById(cmd.idprod).nameprod);
            AddCellToBody(tableLayout, cmd.qteprod.ToString());
            AddCellToBody(tableLayout, cmd.name.ToString());
            AddCellToBody(tableLayout, cmd.phone.ToString());
            AddCellToBody(tableLayout, cmd.phone2.ToString());
            AddCellToBody(tableLayout, cmd.adresse);
            AddCellToBody(tableLayout, cmd.gov);
            AddCellToBody(tableLayout, cmd.code);
            AddCellToBody(tableLayout, cmd.email);

            return tableLayout;
        }
        // Method to add single cell to the Header  
        public void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {

            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK)))
                {
                HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
        });
        }

        // Method to add single cell to the body  
        public void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK)))
                 {
                HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
        });
        }

    }
}



