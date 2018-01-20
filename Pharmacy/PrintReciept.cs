using System;
using System.Data;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data.SqlClient;
using System.Configuration;

namespace Pharmacy
{
    class PrintReciept
    {

        public DataTable CInfo(string a)
        {
            DataTable table = new DataTable();
            string constng = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            SqlConnection Con = new SqlConnection(constng);

            //Con.Open();
            string sqlQuery = @"SELECT * FROM dbo.[Customer] where Customer.CustomerId =" + a + ";";
            using (SqlCommand cmd = new SqlCommand(sqlQuery, Con))
            {
                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(table);
            }


            return table;
        }

        public void ExportDataTableToPdf(String strPdfPath, string strHeader, string CustomerId,string Discount,string Total,string WID,string Paid,string Due)
        {
            System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            
            //Report Header
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntHead = new Font(bfntHead, 16, 1, Color.GRAY);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
            document.Add(prgHeading);



            //Author
            Paragraph prgAuthor = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntAuthor = new Font(btnAuthor, 8, 2, Color.BLACK);
            prgAuthor.Alignment = Element.ALIGN_CENTER;
            prgAuthor.Add(new Chunk("Haji Abdul Rahim Market,Chandona, Chowrasta Bazar(Chaul Potti), Gazipur \nProprietor: 01711453878, 01751872421 \n", fntAuthor));

            Paragraph prgCre = new Paragraph();
            BaseFont btnCre = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntCre = new Font(btnCre, 8, 2, Color.BLACK);
            prgCre.Alignment = Element.ALIGN_LEFT;


            Paragraph date = new Paragraph();
            date.Alignment = Element.ALIGN_RIGHT;
            date.Add(new Chunk("Date : " + DateTime.Now.ToShortDateString() + "\n", fntCre));

            DataTable customer = new DataTable();
            customer = CInfo(CustomerId);

            prgCre.Add(new Chunk("Whlolesale ID : " + WID + "\n", fntCre));
            prgCre.Add(new Chunk("Customer Name : " + customer.Rows[0][1] + "\n", fntCre));
            prgCre.Add(new Chunk("Company Name  : " + customer.Rows[0][2] + "\n", fntCre));
            prgCre.Add(new Chunk("Phone No      : " + customer.Rows[0][3] + "\n", fntCre));

            // prgAuthor.Add(new Chunk("\nRun Date : " + DateTime.Now.ToShortDateString(), fntAuthor));
            document.Add(prgAuthor);

            //Add a line seperation
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Add credentials
            document.Add(date);
            document.Add(prgCre);



            //Add line break
            document.Add(new Chunk("\n", fntHead));

            //Write the table
            SQLCommands sql = new SQLCommands();
            DataTable dtblTable = new DataTable();
            dtblTable = sql.Fillwholesalel(WID);

            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            //Table header
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 10, 1, Color.WHITE);
            for (int i = 0; i < 5; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = Color.GRAY;
                cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                table.AddCell(cell);
            }
            //table Data
            Font subTitleFont = new Font();
            subTitleFont = FontFactory.GetFont("Arial", 10);
            for (int i = 0; i < dtblTable.Rows.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(dtblTable.Rows[i][j].ToString(), subTitleFont));
                    if (j == 4)
                    {

                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table.AddCell(cell);
                    }
                    else
                    {
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.AddCell(cell);
                    }
                }
            }

            PdfPCell cell1 = new PdfPCell(new Phrase("", subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);
            table.AddCell(cell1);
            table.AddCell(cell1);
            // table2.AddCell(new Phrase(" ", fntCre));
            cell1 = new PdfPCell(new Phrase("Sub-Total:", subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase(dtblTable.Compute("SUM(Total)", string.Empty).ToString(), subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);

            cell1 = new PdfPCell(new Phrase("", subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);
            table.AddCell(cell1);
            table.AddCell(cell1);
            // table2.AddCell(new Phrase(" ", fntCre));
            cell1 = new PdfPCell(new Phrase("Discount:", subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase(Discount + "%", subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);

            cell1 = new PdfPCell(new Phrase("", subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);
            table.AddCell(cell1);
            table.AddCell(cell1);
            // table2.AddCell(new Phrase(" ", fntCre));
            cell1 = new PdfPCell(new Phrase("Total:", subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase(Total, subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);

            cell1 = new PdfPCell(new Phrase("", subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);
            table.AddCell(cell1);
            table.AddCell(cell1);
            // table2.AddCell(new Phrase(" ", fntCre));
            cell1 = new PdfPCell(new Phrase("Paid:", subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase(Paid, subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);

            cell1 = new PdfPCell(new Phrase("", subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);
            table.AddCell(cell1);
            table.AddCell(cell1);
            // table2.AddCell(new Phrase(" ", fntCre));
            cell1 = new PdfPCell(new Phrase("Due:", subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase(Due, subTitleFont));
            cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.BorderWidthBottom = 0f;
            cell1.BorderWidthLeft = 0f;
            cell1.BorderWidthTop = 0f;
            cell1.BorderWidthRight = 0f;
            table.AddCell(cell1);

            document.Add(table);


            // PdfPTable table2 = new PdfPTable(8);
            //document.Add(table2);

            ///footer
            Paragraph footer = new Paragraph();
            footer.Alignment = Element.ALIGN_CENTER;
            footer.Add(new Chunk("\n\n\nCustomer's Signature                                                                               Authorized Signature\n\nTHANK YOU FOR YOUR BUSINESS WITH US \nSoftware Distributed by Perky Rabbit Corporation Limited\nFor Support: www.perkyrabbit.com |support@perkyrabbit.com | +88 02 9859491, +8801708518090 \n", subTitleFont));


            document.Add(footer);
            document.Close();
            writer.Close();
            fs.Close();
        }
    }
}
