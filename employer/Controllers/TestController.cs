using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
namespace employer.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult createNew()
        {
            return View();
        }

        public JsonResult saveTest(string title, string description, DateTime startDate, DateTime endDate, int duration)
        {
            Error objError = new Error();
            employeeTest objEmployeeTest = new employeeTest();
            objEmployeeTest.testTitle = title;
            objEmployeeTest.testDescription = description;
            objEmployeeTest.startDate = startDate;
            objEmployeeTest.endDate = endDate;
            objEmployeeTest.testDuration = duration;
            objEmployeeTest.comp
            Test objTest = new Test();
            objError = objTest.createTest(objEmployeeTest);
            return Json(objError, JsonRequestBehavior.AllowGet);
        }
    }
}
