using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mobadrat.Data;
using Mobadrat.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using static Mobadrat.Helper;

namespace Mobadrat.Controllers
{
	public class MobadratFollowController : Controller
	{
		private readonly ApplicationDbContext _db;
		private IWebHostEnvironment _webHostEnvironment;
		public MobadratFollowController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public static int currpage;
		public IActionResult Index(int? page)
		{
			ViewBag.UserType = Convert.ToInt32(HttpContext.Session.GetString("UserTypeId"));
			var pageNumber = page ?? 1;
			int pageSize = 8;
			if (HttpContext.Session.GetString("UserId") != null)
			{
				var GetModadrat = _db.Mobadras.Include(x => x.User).Include(x => x.Branch).Include(x => x.Status)
					.Include(x => x.Status).Include(x => x.Department).Where(x => x.isDeleted == false).OrderByDescending(x => x.CreateDate).ToPagedList(pageNumber, pageSize);
				currpage = pageNumber;
				return View(GetModadrat);
			}
			else
			{
				return RedirectToAction("Index", "Login");
			}
		}

		[HttpGet]
		[NoDirectAccess]
		public async Task<IActionResult> MobadraDetails(int id = 0)
		{
			var mobadra = await _db.Mobadras.Include(x => x.User).Include(x => x.DurationTime).Include(x => x.Volunteer).Include(x => x.Status).
				Include(x => x.Branch).Include(x => x.Department).Include(x => x.Geha_Traget).Where(x => x.isDeleted == false && x.MobadraID == id).FirstOrDefaultAsync();
			if (mobadra == null)
			{
				return NotFound();
			}
			return View(mobadra);
		}

		[HttpGet]
		[NoDirectAccess]
		public async Task<IActionResult> _ShowAttatch(int id)
		{
			var GetAttach = await _db.MobadraUploadfiles.Include(x => x.Mobadra)
				.Where(x => x.isDeleted == false && x.MobadraID == id).OrderByDescending(x => x.CreateDate).ToListAsync();
			if (GetAttach.Count > 0)
			{

			}
			TempData["tempMobadraID"] = id;
			return View(GetAttach);
		}

		public FileResult DownloadFile(string fileName)
		{
			fileName = fileName.Substring(14);
			//Build the File Path.
			string path = Path.Combine(this._webHostEnvironment.WebRootPath, "uploadfiles/") + fileName;

			//Read the File data into Byte Array.
			byte[] bytes = System.IO.File.ReadAllBytes(path);

			//Send the File to Download.
			return File(bytes, "application/octet-stream", fileName);
		}

		[HttpGet]
		[NoDirectAccess]
		public async Task<IActionResult> MobadraDecision(int id = 0)
		{
			ViewBag.StatusID = new SelectList(_db.Statuses.Where(x => x.StatusID == 2 || x.StatusID == 3 || x.StatusID == 4), "StatusID", "StatusName");
			if (id == 0)
			{

				return View(new MobadraComment());
			}
			else
			{
				//var mobadra = await _db.MobadraComments.Include(x => x.Comment).Where(x => x.MobadraID == id).ToListAsync();
				var mobadra = await _db.MobadraComments.FindAsync(id);
				return View(mobadra);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> MobadraDecision(int id, [Bind("RecordID, MobadraID, MobadraCommet, StatusID, UserId")] MobadraComment mobadraComment)
		{
			mobadraComment.MobadraID = id;
			mobadraComment.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
			if (ModelState.IsValid)
			{
				_db.Add(mobadraComment);
				int rowAdded = await _db.SaveChangesAsync();
				if (rowAdded > 0)
				{
					var dataModel = await _db.Mobadras.FindAsync(id);
					dataModel.StatusID = mobadraComment.StatusID;
					_db.Mobadras.Update(dataModel);
					await _db.SaveChangesAsync();
				}
				return Json(new
				{
					isValid = true,
					html = Helper.RenderRazorViewToString(this, "Index", _db.Mobadras.Include(x => x.User).
						Include(x => x.Branch).Include(x => x.Status).Include(x => x.Department).Where(x => x.isDeleted == false).OrderByDescending(x => x.CreateDate).ToPagedList(currpage, 8))
				});
			}

			return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "MobadraDecision", mobadraComment) });
		}

	}
}
