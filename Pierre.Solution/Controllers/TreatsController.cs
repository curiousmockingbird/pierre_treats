using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using PierresBakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PierresBakery.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PierresBakeryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, PierresBakeryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

		public async Task<ActionResult> Index(string sortOrder)
		{
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			switch (sortOrder)
			{
				case "price":
					var sortByPrice = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).OrderByDescending(treat => treat.Price).ToList();
					return View(sortByPrice);
				default:
					var sortByName = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).OrderBy(treat => treat.TreatName).ToList();
					return View(sortByName);
			}
		}

		public async Task<ActionResult> Create()
		{
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			List<Flavor> userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).ToList();
			ViewBag.FlavorId = new SelectList(userFlavors, "FlavorId", "FlavorName");
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(Treat treat, int FlavorId)
		{
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);
			treat.User = currentUser;
			_db.Treats.Add(treat);
			_db.SaveChanges();
      
			if (FlavorId != 0)
			{
				_db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId});
			}
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Details(int id)
		{
			Treat thisTreat = _db.Treats
				.Include(treat => treat.JoinEntities)
				.ThenInclude(join => join.Treat)
				.FirstOrDefault(treat => treat.TreatId == id);
			return View(thisTreat);
		}

		public async Task<ActionResult> Edit(int id)
		{
			Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      
			var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _userManager.FindByIdAsync(userId);

			List<Flavor> userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).ToList();
			ViewBag.FlavorId = new SelectList(userFlavors, "FlavorId", "FlavorName");

			return View(thisTreat);
		}

		[HttpPost]
		public ActionResult Edit(Treat treat, int FlavorId)
		{
			_db.Entry(treat).State = EntityState.Modified;
			_db.SaveChanges();

			foreach(TreatFlavor join in _db.TreatFlavor)
			{
				if(treat.TreatId == join.TreatId && FlavorId == join.FlavorId)
				{
					return RedirectToAction("Details", new {id = treat.TreatId});
				}
			}

			if (FlavorId != 0)
			{
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId});
				_db.SaveChanges();
			}
			return RedirectToAction("Details", new {id = treat.TreatId});
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
			_db.Treats.Remove(thisTreat);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult DeleteFlavor(int joinId)
		{
			TreatFlavor thisJoin = _db.TreatFlavor.FirstOrDefault(join => join.TreatFlavorId == joinId);
			_db.TreatFlavor.Remove(thisJoin);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

  }
}