using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobadrat.Data;
using System.Linq;
using System;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Mobadrat.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            HttpContext.Session.Remove("UserFullname");
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            //if (ModelState.IsValid)
            //{
            //    var GetUser = _db.Users.Where(x => x.UserName == username && x.UserPassword == password
            //            && x.isDeleted == false && x.isActive == true).ToList();
            //    if (GetUser != null && GetUser.Count > 0)
            //    {
            //        HttpContext.Session.SetString("UserFullname", GetUser.First().UserFullName);
            //        HttpContext.Session.SetString("UserTypeId", Convert.ToInt32(GetUser.First().UserType).ToString());
            //        HttpContext.Session.SetString("UserId", Convert.ToInt32(GetUser.First().UserId).ToString());
            //        HttpContext.Session.SetString("BranchID", Convert.ToInt32(GetUser.First().BranchID).ToString());
            //        //ViewBag.UserTypeId = HttpContext.Session.GetString("UserTypeId");
            //        return RedirectToAction("Index", "Home");
            //    }

            //}
            //else
            //{

            //}
            GetEmpData();
            return View();
        }

        protected bool GetEmpData()
        {
            //string connectionString = "User Id=APPS;Password=AppsaU8e;Data Source=10.11.42.119:1523/UAT;Pooling=true;";
            string connectionString = "User Id=XXX_TRASOL_INT;Password=XXX_TRASOL_INT;Data Source=10.11.12.36:1521/PROD;Pooling=true;";

            using (OracleConnection con = new OracleConnection(connectionString))

            {
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "SELECT EMPLOYEE_ID ,EMPLOYEE_FULL_NAME_AR, SEX from XXX_TRASOL_INT.XXX_HR_TRASEL_EMP_INFO where EMPLOYEE_ID = 18085";
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
                            string name = dr["EMPLOYEE_FULL_NAME_AR"].ToString();
                            string sex = dr["SEX"].ToString();
                            string identity = dr["NATIONAL_IDENTIFIER"].ToString();
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

