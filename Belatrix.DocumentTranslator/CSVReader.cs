using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Belatrix.DocumentTranslator
{
    public class CSVReader : ICSVReader
    {
        public List<T> Read<T>(HttpPostedFileBase file) where T : class
        {
            using (TextReader textReader = new StreamReader(file.InputStream))
            {
                return Read<T>(textReader);
            }
        }

        public List<T> Read<T, M>(HttpPostedFileBase file) 
            where T : class 
            where M : class
        {
            using (TextReader textReader = new StreamReader(file.InputStream))
            {
                return Read<T, M>(textReader);
            }
        }

        public List<T> Read<T, M>(TextReader file) 
            where T: class
            where M: class
        {
            var csv = new CsvHelper.CsvReader(file);
            DefaultConfiguration(csv);
            csv.Configuration.RegisterClassMap(typeof(M));
            return csv.GetRecords<T>().ToList();
        }

        public List<T> Read<T>(TextReader file) where T : class
        {
            Configuration c = new Configuration() {
                BadDataFound = null
            };
            var csv = new CsvHelper.CsvReader(file, c);
            DefaultConfiguration(csv);
            return csv.GetRecords<T>().ToList();
        }

        private void DefaultConfiguration(CsvReader csv) {
            csv.Configuration.HasHeaderRecord = true;
            csv.Configuration.Delimiter = ";";
            
            csv.Configuration.IgnoreBlankLines = true;
        }
    }
}
