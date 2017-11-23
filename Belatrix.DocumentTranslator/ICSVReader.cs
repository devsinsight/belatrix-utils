using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Belatrix.DocumentTranslator
{
    public interface ICSVReader
    {
        List<T> Read<T>(HttpPostedFileBase file) where T : class;
        List<T> Read<T, M>(HttpPostedFileBase file) where T : class where M : class;
    }
}
