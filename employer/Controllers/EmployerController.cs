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
        
        public ActionResult Index()
        {
            EmpAccount objEmpAccount = new EmpAccount();
            account objAccount = objEmpAccount.getEmployerDetail(Convert.ToInt64(User.Identity.Name));
            return View(objAccount);
        }

        public ActionResult Company()
        {
            EmpAccount objEmpAccount = new EmpAccount();
            company objCompany = objEmpAccount.getEmployerCompany(Convert.ToInt64(User.Identity.Name));
            return View(objCompany);
        }

    }
}
