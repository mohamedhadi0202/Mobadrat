using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobadrat.Data;
using System;
using System.Linq;

namespace Mobadrat.Controllers
{
	public class InfoController : Controller
	{
		private readonly ApplicationDbContext _db;
		private IWebHostEnvironment _webHostEnvironment;
		public InfoController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			if (HttpContext.Session.GetString("UserId") != null)
			{
				ViewBag.UserType = Convert.ToInt32(HttpContext.Session.GetString("UserTypeId"));
				var GetAllUserData = _db.Users.Where(x => x.isDeleted == false && x.isActive == true && x.IdentityNumber == Convert.ToInt32(HttpContext.Session.GetString("IdentityNumber"))).FirstOrDefault();
				return View(GetAllUserData);
			}
			return RedirectToAction("Index", "Login");
		}
	}
}
