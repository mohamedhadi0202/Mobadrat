using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobadrat.Data;
using Mobadrat.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mobadrat.Controllers
{
	public class RegisterController : Controller
	{
		private readonly ApplicationDbContext _db;
		private IWebHostEnvironment _webHostEnvironment;
		public RegisterController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			return View();
		}

		//[HttpPost]
		//public IActionResult Index(int IdentityNumber, Models.User user)
		//{
		//	var checkUser = _db.Users.FirstOrDefault(u => u.IdentityNumber == user.IdentityNumber);
		//	if (checkUser != null)
		//		return Json(new { isExistInDB = true });

		//	return RedirectToAction("CreateUser", "Register");
		//}

		public IActionResult GetAllData(string userId)
		{
			//TempData["succsess"] = "";
			GetEmpData(userId);
			getCurrentDateHijry();
			return PartialView("~/Views/Register/_GetAllData.cshtml");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> PostData(User user)
		{
			if (ModelState.IsValid)
			{
				_db.Add(user);
				var dataAffected = await _db.SaveChangesAsync();
				if (dataAffected > 0)
				{
					TempData["succsess"] = "add";
					return RedirectToAction("Index", "Login");
				}
				return View("InsertSucsses");
			}
			else
			{

				//var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
			}
			return View("Index");

		}

		[HttpGet]
		public IActionResult CreateUser(string userIdValue, User user)
		{
			bool CheckEmployee = GetEmpData(userIdValue);
			if (!CheckEmployee)
			{
				return RedirectToAction("Index", "Login");
			}
			getCurrentDateHijry();
			return View("CreateUser", user);
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser(User user)
		{
			var checkUser = _db.Users.FirstOrDefault(u => u.IdentityNumber == user.IdentityNumber);
			if (checkUser == null)
			{
				return Json(new { isExistInDB = true });
			}
			else
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Enter required fields");
				}
				else
				{
					_db.Add(user);
					var dataAffected = await _db.SaveChangesAsync();
					if (dataAffected > 0)
					{
						//TempData["succsess"] = "add";
					}
				}
			}
			

			return Json(user);
		}

		public void getCurrentDateHijry()
		{
			CultureInfo arSA = CultureInfo.CreateSpecificCulture("ar-SA");
			DateTime date = DateTime.Now;
			TempData["CurrentHijryDate"] = date.ToString("dd/MM/yyyy", arSA);
		}

		public IActionResult InsertSucsses()
		{
			return View();
		}

		protected bool GetEmpData(string id)
		{
			if (id == null)
				return false;

			if (id.Count() == 0 || id.Count() < 10)
			{
				return false;
			}
			else
			{
				//string connectionString = "User Id=APPS;Password=AppsaU8e;Data Source=10.11.42.119:1523/UAT;Pooling=true;";
				string connectionString = "User Id=XXX_TRASOL_INT;Password=XXX_TRASOL_INT;Data Source=10.11.12.36:1521/PROD;Pooling=true;";

				using (OracleConnection con = new OracleConnection(connectionString))
				{
					OracleCommand cmd = new OracleCommand();
					cmd.CommandText = "SELECT ACTUAL_ORG_ID, EMPLOYEE_ID,EMAIL_ADDRESS, MOBILE, EMPLOYEE_FULL_NAME_AR, NATIONAL_IDENTIFIER, SEX from XXX_TRASOL_INT.XXX_HR_TRASEL_EMP_INFO where XXX_TRASOL_INT.XXX_HR_TRASEL_EMP_INFO.NATIONAL_IDENTIFIER = " + id;

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
								ViewBag.NATIONAL_IDENTIFIER = dr["NATIONAL_IDENTIFIER"].ToString();
								ViewBag.EMPLOYEE_ID = dr["EMPLOYEE_ID"].ToString();
								ViewBag.Emp_Full_Name = dr["EMPLOYEE_FULL_NAME_AR"].ToString();
								ViewBag.SEX = dr["SEX"].ToString();
								ViewBag.MOBILE = dr["MOBILE"].ToString();
								ViewBag.EMAIL_ADDRESS = dr["EMAIL_ADDRESS"].ToString();
								ViewBag.ORGANIZATION_ID = dr["ACTUAL_ORG_ID"].ToString();

							}
						}

						OracleCommand cmd1 = new OracleCommand();
						cmd1.CommandText = "SELECT ORGANIZATION_ID,ORG_NAME,MANAGER_NAME,PARENT_ORG,PARENT_ORG_ID from XXX_TRASOL_INT.XXX_HR_TRASEL_ORG where XXX_TRASOL_INT.XXX_HR_TRASEL_ORG.ORGANIZATION_ID = " + ViewBag.ORGANIZATION_ID;

						cmd1.Connection = con;
						cmd1.CommandType = CommandType.Text;
						OracleDataReader dr1 = cmd1.ExecuteReader();
						if (dr1.HasRows)
						{
							while (dr1.Read())
							{
								ViewBag.ORGANIZATION_ID = dr1["ORGANIZATION_ID"].ToString();
								ViewBag.ORG_NAME = dr1["ORG_NAME"].ToString();
								ViewBag.PARENT_ORG_ID = dr1["PARENT_ORG_ID"].ToString();
								ViewBag.PARENT_ORG = dr1["PARENT_ORG"].ToString();

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
}
