using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.Business.Interfaces
{
    public interface IDocumentService
    {
        byte[] ExcelAktar<T>(List<T> list) where T : class, new();
        string PdfAktar<T>(List<T> list) where T : class, new();
    }
}
