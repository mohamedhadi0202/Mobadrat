using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace Mobadrat.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetAllData(string IdentityNumber)
        {
            GetEmpData(IdentityNumber);
            return View("Index");
        }

        protected bool GetEmpData(string id)
        {
            //string connectionString = "User Id=APPS;Password=AppsaU8e;Data Source=10.11.42.119:1523/UAT;Pooling=true;";
            string connectionString = "User Id=XXX_TRASOL_INT;Password=XXX_TRASOL_INT;Data Source=10.11.12.36:1521/PROD;Pooling=true;";

            using (OracleConnection con = new OracleConnection(connectionString))

            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "SELECT EMPLOYEE_ID , MOBILE, EMPLOYEE_FULL_NAME_AR, NATIONAL_IDENTIFIER, SEX from XXX_TRASOL_INT.XXX_HR_TRASEL_EMP_INFO where NATIONAL_IDENTIFIER = " + id;
                cmd.Connection = con;
                con.Open();
                try
                {
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //ViewBag["Emp_Full_Name"] = "112345";
                            ViewBag.Emp_Full_Name = dr["EMPLOYEE_FULL_NAME_AR"].ToString();
                            string sex = dr["SEX"].ToString();
                            string identity = dr["NATIONAL_IDENTIFIER"].ToString();
                            ViewBag.MOBILE = dr["MOBILE"].ToString();
                        }

                        return true;
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    return false;
                }

                return false;
            }

        }
    }
}
