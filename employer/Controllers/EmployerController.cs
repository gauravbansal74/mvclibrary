using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using DLL.ViewModel;

namespace employer.Controllers
{
    public class EmployerController : Controller
    {
        int userId = 1;
        public ActionResult Index()
        {
            EmpAccount objEmpAccount = new EmpAccount();
            account objAccount = objEmpAccount.getEmployerDetail(userId);
            return View(objAccount);
        }

        public ActionResult Company()
        {
            EmpAccount objEmpAccount = new EmpAccount();
            company objCompany = objEmpAccount.getEmployerCompany(userId);
            return View(objCompany);
        }

    }
}
