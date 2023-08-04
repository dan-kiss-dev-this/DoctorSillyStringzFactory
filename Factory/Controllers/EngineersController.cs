using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;
    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Engineer> allEngineers = _db.Engineers.ToList();
      return View(allEngineers);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer)
    {
      // can add error handeling if engineer is blank-data annotations
      _db.Engineers.Add(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      //error handeling if null
      Engineer engineer = _db.Engineers
        .Include(engineer => engineer.AuthorizationJoins)
        .ThenInclude(joinEntry => joinEntry.Machine)
        .FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(engineer);
    }
  }
}