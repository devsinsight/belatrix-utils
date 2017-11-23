using Belatrix.DocumentTranslator;
using Belatrix.DocumentTranslator.Map;
using Belatrix.DocumentTranslator.Models;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Belatrix.Web.Controllers
{
    public class ExampleController : Controller
    {
        private static IExcelReader excel;

        public ExampleController()
        {
            excel = new ExcelReader();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadExcel(HttpPostedFileBase file)
        {
            return Json(excel.Read<Example>(file, "Sheet1"));
        }
    }
}
