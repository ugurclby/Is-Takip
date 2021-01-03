using FastMember;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using YSKProje.ToDo.Business.Interfaces;

namespace YSKProje.ToDo.Business.Concrete
{
    public class DocumentManager : IDocumentService
    {
        // Excel işlemleri içi Epplus kullanıldı
     /// <summary>
    /// Byte tipinde excel verisi döner
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
        public byte[] ExcelAktar<T>(List<T> list) where T : class, new()
        {
            var excelPackage = new ExcelPackage();
            var excelBlank= excelPackage.Workbook.Worksheets.Add("Calisma1");
            excelBlank.Cells["A1"].LoadFromCollection(list, true, OfficeOpenXml.Table.TableStyles.Light15);
            return excelPackage.GetAsByteArray(); 
        }
        // Pdf işlemleri için itextsharp core kullanıldı ve Fat member kullanılarak ObjectReader ile liste tipi data table direk atıldı.
        /// <summary>
        /// Pdf dosyasını kayıt edildiği dizin döner
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public string PdfAktar<T>(List<T> list) where T : class, new()
        {
            DataTable dt = new DataTable();
            dt.Load(ObjectReader.Create(list));

            var fileName = Guid.NewGuid() + ".pdf";
            var returnPath = "/documents/" + fileName;
            var path = Path.Combine(Directory.GetCurrentDirectory()+"/wwwroot/documents/"+fileName);
            var stream = new FileStream(path, FileMode.Create);
            
            string arialTtf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(arialTtf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font font = new Font(bf, 12, Font.NORMAL);

            Document document = new Document(PageSize.A4,25f, 25f, 25f, 25f); 
            PdfWriter.GetInstance(document, stream);
            document.Open();

            PdfPTable pdfPTable = new PdfPTable(dt.Columns.Count);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                pdfPTable.AddCell(new Phrase(dt.Columns[i].ColumnName, font));
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int y = 0; y < dt.Columns.Count; y++)
                {
                    pdfPTable.AddCell(new Phrase(dt.Rows[i][y].ToString(),font));
                }
            }

            document.Add(pdfPTable);

            document.Close(); 
            return returnPath;
        }
    }
}
