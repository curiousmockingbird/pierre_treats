using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PierresBakery.Models;

namespace PierresBakery.Controllers
{
  public class HomeController : Controller
  {
    private readonly PierresBakeryContext _db;

    public HomeController(PierresBakeryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      ViewBag.Treat = new List<Treat>(_db.Treats);
      return View(_db.Flavors.ToList());
    }
  }
  
}