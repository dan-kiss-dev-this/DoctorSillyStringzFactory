using Microsoft.AspNetCore.Mvc;
using Factory.Models;

namespace DoctorSillyStringzFactory.Controllers;

public class HomeController : Controller
{
  private readonly FactoryContext _db;
  public HomeController(FactoryContext db)
  {
    _db = db;
  }

  public IActionResult Index()
  {
    List<Engineer> allEngineers = _db.Engineers.ToList();
    List<Machine> allMachines = _db.Machines.ToList();
    ViewBag.allEngineers = allEngineers;
    ViewBag.allMachines = allMachines;
    return View();
  }
}
