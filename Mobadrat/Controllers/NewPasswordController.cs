using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mobadrat.Data;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mobadrat.Controllers
{
	public class NewPasswordController : Controller
	{
		private readonly ApplicationDbContext _db;
		private IWebHostEnvironment _webHostEnvironment;
		public NewPasswordController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			//var GetUser = _db.Users.Where(x => x.UserId == Convert.ToInt32(HttpContext.Session.GetString("UserId"))
			//			&& x.isDeleted == false && x.isActive == true).FirstOrDefault();
			ViewBag.UserType = Convert.ToInt32(HttpContext.Session.GetString("UserTypeId"));
			TempData["Referrer"] = "";
			if (HttpContext.Session.GetString("UserId") != null)
			{
				return View();
			}
			return RedirectToAction("Index", "Login");
		}

		[HttpPost, ActionName("Index")]
		public async Task<IActionResult> Index(string UserPassword, string ConfirmUserPassword)
		{
			if (ConfirmUserPassword != null || UserPassword != null)
			{
				if (UserPassword == ConfirmUserPassword)
				{
					var dataModel = await _db.Users.FindAsync(Convert.ToInt32(HttpContext.Session.GetString("UserId")));
					if(dataModel != null)
					{
						dataModel.UserPassword = UserPassword;
						_db.Users.Update(dataModel);
						await _db.SaveChangesAsync();
						//TempData["Referrer"] = "SaveSuccess";
						//return RedirectToAction("Index");
					}
				}
			}
			return View();
		}
			
	}
}

