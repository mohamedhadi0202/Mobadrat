using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mobadrat.Data;
using Mobadrat.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mobadrat.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _db;
		private IWebHostEnvironment _webHostEnvironment;

		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_logger = logger;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

		public IActionResult Index(int CountAll)
		{
			if (HttpContext.Session.GetString("UserId") != null)
			{
				ViewBag.UserType = Convert.ToInt32(HttpContext.Session.GetString("UserTypeId"));
				int AllCount = _db.Mobadras.Where(s => s.isDeleted == false).Count();
				if (AllCount >= 0)
				{
                    TempData["AllCount"] = AllCount;
				}

                int AllCountNew = _db.Mobadras.Where(s => s.isDeleted == false && s.StatusID == 1).Count();
                if (AllCountNew >= 0)
                {
                    TempData["AllCountNew"] = AllCountNew;
                }

                int AllCountUnderProcess = _db.Mobadras.Where(s => s.isDeleted == false && s.StatusID == 2).Count();
                if (AllCountUnderProcess >= 0)
                {
                    TempData["AllCountUnderProcess"] = AllCountUnderProcess;
                }

                int AllCountApproved = _db.Mobadras.Where(s => s.isDeleted == false && s.StatusID == 3).Count();
                if (AllCountApproved >= 0)
                {
                    TempData["AllCountUnderApproved"] = AllCountApproved;
                }

                int AllCountRejected = _db.Mobadras.Where(s => s.isDeleted == false && s.StatusID == 4).Count();
                if (AllCountRejected >= 0)
                {
                    TempData["AllCountUnderRejected"] = AllCountRejected;
                }

                int AllCountToday = _db.Mobadras.Where(s => s.isDeleted == false && s.StatusID == 4 && s.CreateDate == DateTime.Now).Count();
                if (AllCountToday >= 0)
                {
                    TempData["AllCountToday"] = AllCountToday;
                }

                int AllUserRegistered = _db.Users.Where(s => s.isDeleted == false).Count();
                if (AllUserRegistered >= 0)
                {
                    TempData["AllUserRegistered"] = AllUserRegistered;
                }

                return View();
			}
			return RedirectToAction("Index", "Login");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
