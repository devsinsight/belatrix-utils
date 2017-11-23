using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Belatrix.DocumentTranslator
{
    public interface IExcelReader
    {
        List<T> Read<T>(HttpPostedFileBase file, string worksheetName=null) where T : class;
    }
}
