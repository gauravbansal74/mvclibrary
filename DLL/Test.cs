using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class Test
    {
        private offcampus4uEntities db;

        public Error createTest(employeeTest objEmployeeTest)
        {
            Error objError = new Error();
            try
            {
                db = new offcampus4uEntities();
                objEmployeeTest.createdOn = DateTime.Now;
                objEmployeeTest.modifiedOn = DateTime.Now;
                db.employeeTests.Add(objEmployeeTest);
                db.SaveChanges();
                objError.isSuccess = true;
                objError.message = "Success";
            }
            catch (Exception ex)
            {
                objError.isSuccess = false;
                objError.message = ex.Message;
            }
            return objError;
        }
    }
}
