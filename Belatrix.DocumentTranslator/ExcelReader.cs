using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Belatrix.DocumentTranslator
{
    public class ExcelReader : IExcelReader
    {
        public List<T> Read<T>(HttpPostedFileBase file, string worksheetName = null) where T : class
        {
            using (XLWorkbook workbook = new XLWorkbook(file.InputStream, XLEventTracking.Disabled))
            {
                IXLWorksheet worksheet = workbook.Worksheets.First( ws => ws.Name == (worksheetName ?? ws.Name));

                return ReadingFromExcelWorksheet<T>(worksheet);
            }
        }

        private List<T> ReadingFromExcelWorksheet<T>(IXLWorksheet worksheet) where T: class {
            using (var ms = new MemoryStream())
            {
                using (TextWriter tw = new StreamWriter(ms, Encoding.UTF8))
                {
                    CsvWriteOnMemoryStream(tw, worksheet);

                    return CsvReadFromMemoryStream<T>(ms);
                }
            }
        }

        private void CsvWriteOnMemoryStream(TextWriter tw, IXLWorksheet worksheet) {
            var csv = new CsvWriter(tw);

            foreach (IXLRow row in worksheet.Rows())
            {
                foreach (IXLCell cell in row.Cells())
                {
                    csv.WriteField(cell.Value.ToString());
                }
                csv.NextRecord();
            }

            tw.Flush();
        }

        private List<T> CsvReadFromMemoryStream<T>(MemoryStream ms) where T: class {
            ms.Position = 0;

            using (var reader = new CsvReader(new StreamReader(ms)))
            {
                return reader.GetRecords<T>().ToList();

            }
        }
    }
}
