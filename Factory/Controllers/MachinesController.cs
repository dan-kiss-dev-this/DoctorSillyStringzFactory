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

    public ActionResult AddEngineer(int id)
    {
      // we make a form, associate it to an engineer id
      // we add a viewbag to allow a machine to be selected
      Machine machine = _db.Machines
      .FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(machine);
    }

    [HttpPost]
    public ActionResult AddEngineer(Machine machine, int engineerId)
    {
      // id is engineers note add machine will go into the AuthorizationJoin table
#nullable enable
      AuthorizationJoin? alreadyJoined = _db.AuthorizationJoins.FirstOrDefault(join => (join.EngineerId == engineerId && join.MachineId == machine.MachineId));
#nullable disable
      // id of 0 is default so remove this corner case
      if (alreadyJoined == null && engineerId != 0)
      {
        _db.AuthorizationJoins.Add(new AuthorizationJoin() { MachineId = machine.MachineId, EngineerId = engineerId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = machine.MachineId });
    }
  }
}