using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobadrat.Data;
using Mobadrat.Models;
using System.Threading.Tasks;
using static Mobadrat.Helper;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Mobadrat.Models.ViewModels;
using System.IO;
using Microsoft.VisualBasic;

namespace Mobadrat.Controllers
{
	public class MobadraController : Controller
	{
		private readonly ApplicationDbContext _db;
		private IWebHostEnvironment _webHostEnvironment;
		public MobadraController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public static int currpage;
		public IActionResult Index(int? page)
		{
			if (HttpContext.Session.GetString("UserId") != null)
			{
				ViewBag.UserType = Convert.ToInt32(HttpContext.Session.GetString("UserTypeId"));
				var pageNumber = page ?? 1;
				int pageSize = 5;
				var GetMobadra = _db.Mobadras.Include(x => x.Status).
									Include(x => x.DurationTime).Where(x => x.isDeleted == false).OrderByDescending(x => x.CreateDate).ToPagedList(pageNumber, pageSize);
				currpage = pageNumber;
				ViewBag.Geha_TragetID = new SelectList(_db.geha_Tragets, "Geha_TragetID", "Geha_TragetName");
				ViewBag.VolunteerID = new SelectList(_db.volunteers, "VolunteerID", "VolunteerName");
				ViewBag.DurationTimeID = new SelectList(_db.DurationTimes, "DurationTimeID", "DurationTimeName");
				HttpContext.Session.SetString("_MobadraIDSession", "");
				//ViewBag.BranchID = Convert.ToInt32(HttpContext.Session.GetString("BranchID"));
				//ViewBag.DepartmentId = Convert.ToInt32(HttpContext.Session.GetString("DepartmentId"));
				return View(GetMobadra);
			}
			return RedirectToAction("Index", "Login");
		}

		[HttpGet]
		[NoDirectAccess]
		public async Task<IActionResult> AddOrEdit(int id = 0)
		{
			if (id == 0)
			{

				return View(new Mobadra());
			}
			else
			{
				var mobadra = await _db.Mobadras.FindAsync(id);
				if (mobadra == null)
				{
					return NotFound();
				}
				return View(mobadra);
			}
		}
		[HttpGet]
		[NoDirectAccess]
		public async Task<IActionResult> _Edit(int id = 0)
		{
			ViewBag.Geha_TragetID = new SelectList(_db.geha_Tragets, "Geha_TragetID", "Geha_TragetName");
			ViewBag.VolunteerID = new SelectList(_db.volunteers, "VolunteerID", "VolunteerName");
			ViewBag.DurationTimeID = new SelectList(_db.DurationTimes, "DurationTimeID", "DurationTimeName");
			var mobadra = await _db.Mobadras.FindAsync(id);
			if (mobadra == null)
			{
				return NotFound();
			}
			return View(mobadra);
		}

		[HttpPost]
		[NoDirectAccess]
		public async Task<IActionResult> _Edit(int id, [Bind("MobadraID, MobadraTitle, MobadraDesc, BranchID, DepartmentId, Geha_TragetID,MobadraTarget,MobadraImplement, VolunteerID, DurationTimeID, StatusID, CurrentSpotID, UserId")] Mobadra mobadra)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_db.Update(mobadra);
					await _db.SaveChangesAsync();
					ViewBag.Geha_TragetID = new SelectList(_db.geha_Tragets, "Geha_TragetID", "Geha_TragetName", 0);
					ViewBag.VolunteerID = new SelectList(_db.volunteers, "VolunteerID", "VolunteerName", 0);
					ViewBag.DurationTimeID = new SelectList(_db.DurationTimes, "DurationTimeID", "DurationTimeName", 0);
				}

			}
			catch (DbUpdateConcurrencyException)
			{
				if (!MobadraExists(mobadra.MobadraID))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return Json(new
			{
				isValid = true,
				html = Helper.RenderRazorViewToString(this, "Index", _db.Mobadras.Include(x => x.Status).
								Include(x => x.DurationTime).Where(x => x.isDeleted == false).OrderByDescending(x => x.CreateDate).ToPagedList(currpage, 8))
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddOrEdit(int id, string MobadraTitle, NewAttachmentViewModel model, [Bind("MobadraID, MobadraTitle, MobadraDesc, BranchID, DepartmentId, Geha_TragetID,MobadraTarget,MobadraImplement, VolunteerID, DurationTimeID, StatusID, CurrentSpotID, UserId")] Mobadra mobadra)
		{
			mobadra.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
			mobadra.BranchID = Convert.ToInt32(HttpContext.Session.GetString("BranchID"));
			mobadra.DepartmentId = Convert.ToInt32(HttpContext.Session.GetString("DepartmentId"));
			var getTitle = _db.Mobadras.Where(x => x.MobadraTitle == mobadra.MobadraTitle).FirstOrDefault();
			
			if (getTitle != null)
			{
				return Json(new { isNotExist = false });
			}
			else
			{
				if (ModelState.IsValid)
				{
					if (id == 0)
					{
						mobadra.DepartmentId = mobadra.DepartmentId;
						_db.Add(mobadra);
						int rowAdded = await _db.SaveChangesAsync();
						if (rowAdded > 0)
						{
							//int ddd = mobadra.MobadraID;
							string imagename = "~/uploadfiles/" + UploadFiles(model);
							MobadraUploadfile uploadfileModel = new MobadraUploadfile
							{
								FilePath = imagename,
								MobadraID = mobadra.MobadraID,
								UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"))
							};
							_db.MobadraUploadfiles.Add(uploadfileModel);
							_db.SaveChanges();
						}
					}
					else
					{
						try
						{
							//mobadra.DepartmentId = mobadra.DepartmentId;
						}
						catch (DbUpdateConcurrencyException)
						{
							if (!MobadraExists(mobadra.MobadraID))
							{
								return NotFound();
							}
							else
							{
								throw;
							}
						}
					}

					return Json(new
					{
						isValid = true,
						html = Helper.RenderRazorViewToString(this, "Index", _db.Mobadras.Include(x => x.Status).
									Include(x => x.DurationTime).Where(x => x.isDeleted == false).OrderByDescending(x => x.CreateDate).ToPagedList(currpage, 8))
					});
				}
			}

			return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_IndexPartial", mobadra) });
		}

		private bool MobadraExists(int id)
		{
			return _db.Mobadras.Any(e => e.MobadraID == id);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var dataModel = await _db.Mobadras.FindAsync(id);
			dataModel.isDeleted = true;
			_db.Mobadras.Update(dataModel);
			await _db.SaveChangesAsync();
			return Json(new
			{
				html = Helper.RenderRazorViewToString(this, "Index", _db.Mobadras.Include(x => x.Status).
								Include(x => x.DurationTime).Where(x => x.isDeleted == false).OrderByDescending(x => x.CreateDate).ToPagedList(currpage, 8))
			});
		}

		[HttpGet]
		[NoDirectAccess]
		public async Task<IActionResult> AddAttatchIndex(int id)
		{
			ViewBag.tempMobadraID = id;
			HttpContext.Session.SetString("_MobadraIDSession", id.ToString());
			var GetAttach = await _db.MobadraUploadfiles.Include(x => x.Mobadra)
				.Where(x => x.isDeleted == false && x.MobadraID == id).OrderByDescending(x => x.CreateDate).ToListAsync();
			if (GetAttach.Count > 0)
			{

			}
			TempData["tempMobadraID"] = id;
			return View(GetAttach);
		}

		[HttpGet]
		[NoDirectAccess]
		public async Task<IActionResult> _ShowAttatch(int id)
		{
			var GetAttach = await _db.MobadraUploadfiles.Include(x => x.Mobadra)
				.Where(x => x.isDeleted == false && x.MobadraID == Convert.ToInt32(HttpContext.Session.GetString("_MobadraIDSession"))).OrderByDescending(x => x.CreateDate).ToListAsync();
			if (GetAttach.Count > 0)
			{

			}
			TempData["tempMobadraID"] = id;
			return View(GetAttach);
		}

		[HttpGet]
		[NoDirectAccess]
		public ActionResult CreateAttatch()
		{
			return View(new NewAttachmentViewModel());

		}

		[HttpPost]
		public async Task<IActionResult> CreateAttatch(NewAttachmentViewModel model)
		{
			try
			{
				if (model.FilePath != null)
				{
					string imagename = "~/uploadfiles/" + UploadFiles(model);
					MobadraUploadfile uploadfileModel = new MobadraUploadfile
					{
						FilePath = imagename,
						MobadraID = Convert.ToInt32(HttpContext.Session.GetString("_MobadraIDSession")), //(int)TempData["tempMobadraID"],
						UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"))
					};

					string Extension = Path.GetExtension(model.FilePath.FileName);
					if (Extension != ".pdf")
					{
						TempData["MyData"] = "NotPDF";
					}
					else
					{
						_db.MobadraUploadfiles.Add(uploadfileModel);
						await _db.SaveChangesAsync();
					}

					//return Json(new
					//{
					//	isValid = true,
					//	html = Helper.RenderRazorViewToString(this, "AddAttatchIndex", _db.MobadraUploadfiles.Include(x => x.Mobadra)
					//			.Where(x => x.isDeleted == false && x.MobadraID == (int)TempData["tempMobadraID"]).OrderByDescending(x => x.CreateDate).ToPagedList(currpage, 8))
					//});
				}
			}
			catch (Exception ex)
			{

			}

			return Json(new
			{
				isValid = true,
				html = Helper.RenderRazorViewToString(this, "Index", _db.Mobadras.Include(x => x.Status).
									Include(x => x.DurationTime).Where(x => x.isDeleted == false).OrderByDescending(x => x.CreateDate).ToPagedList(currpage, 8))
			});

		}

		public string UploadFiles(NewAttachmentViewModel model)
		{
			string newFileName = null;

			if (model.FilePath != null)
			{
				string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploadfiles");
				newFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.FilePath.FileName);
				string fullPath = Path.Combine(filePath, newFileName);

				using (var fStream = new FileStream(fullPath, FileMode.Create))
				{
					model.FilePath.CopyTo(fStream);
				}
			}
			return newFileName;
		}

		[HttpPost, ActionName("DeleteAttatch")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteAttatchConfirmed(int id)
		{
			var dataModel = await _db.MobadraUploadfiles.FindAsync(id);
			dataModel.isDeleted = true;
			_db.MobadraUploadfiles.Update(dataModel);
			await _db.SaveChangesAsync();
			return Json(new
			{
				html = Helper.RenderRazorViewToString(this, "Index", _db.Mobadras.Include(x => x.Status).
								Include(x => x.DurationTime).Where(x => x.isDeleted == false).OrderByDescending(x => x.CreateDate).ToPagedList(currpage, 8))
			});
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

	}
}
