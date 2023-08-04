using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;
    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Machine> allMachines = _db.Machines.ToList();
      return View(allMachines);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine)
    {
      // can add error handeling if engineer is blank-data annotations
      _db.Machines.Add(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      //error handeling if null
      Machine machine = _db.Machines
        .Include(machine => machine.AuthorizationJoins)
        .ThenInclude(joinEntry => joinEntry.Engineer)
        .FirstOrDefault(machine => machine.MachineId == id);
      return View(machine);
    }
  }
}