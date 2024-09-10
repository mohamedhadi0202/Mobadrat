using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobadrat.Data;
using System.Linq;
using System;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Mobadrat.Models;

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
			HttpContext.Session.Remove("UserTypeId");
			HttpContext.Session.Remove("UserId");
			HttpContext.Session.Remove("UserFullname");
			HttpContext.Session.Remove("IdentityNumber");
			HttpContext.Session.Remove("BranchID");
			return View();
		}

		[HttpPost]
		public IActionResult Index(string UserName, string UserPassword)
		{
            var GetUser = _db.Users.Where(x => x.UserName == UserName && x.UserPassword == UserPassword
                        && x.isDeleted == false && x.isActive == true).ToList();

            if (GetUser == null || GetUser.Count() == 0)
            {
                return Json(new { isExist = false });
            }
			else
			{
                if (ModelState.IsValid)
                {
                    HttpContext.Session.SetString("UserTypeId", Convert.ToInt32(GetUser.First().UserTypeId).ToString());
					HttpContext.Session.SetString("UserFullname", GetUser.First().EmployeeFullName);
					HttpContext.Session.SetString("UserId", Convert.ToInt32(GetUser.First().UserId).ToString());
					HttpContext.Session.SetString("BranchID", Convert.ToInt32(GetUser.First().BranchID).ToString());
					HttpContext.Session.SetString("DepartmentId", Convert.ToInt32(GetUser.First().DepartmentID).ToString());
					HttpContext.Session.SetString("IdentityNumber", Convert.ToInt32(GetUser.First().IdentityNumber).ToString());
					if (HttpContext.Session.GetString("UserTypeId") == "3")
					{
                        //return RedirectToAction("Index", "Mobadra");
                        return Json(new { isRegularUser = true });
					}
					else if (HttpContext.Session.GetString("UserTypeId") == "2")
					{
                        //return RedirectToAction("Index", "Home");
                        return Json(new { isAdmin = true });
					}
					else if (HttpContext.Session.GetString("UserTypeId") == "1")
					{
						return Json(new { isSuperAdmin = true });
					}
					//return RedirectToAction("Index", "Home");


				}
                else
                {

                }
            }
            

			return View();
		}

	}
}

