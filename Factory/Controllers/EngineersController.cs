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

    public ActionResult AddMachine(int id)
    {
      // we make a form, associate it to an engineer id
      // we add a viewbag to allow a machine to be selected
      Engineer engineer = _db.Engineers
      .FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Title");
      return View(engineer);
    }

    [HttpPost]
    public ActionResult AddMachine(Engineer engineer, int machineId)
    {
      // id is engineers note add machine will go into the AuthorizationJoin table
#nullable enable
      AuthorizationJoin? alreadyJoined = _db.AuthorizationJoins.FirstOrDefault(join => (join.MachineId == machineId && join.EngineerId == engineer.EngineerId));
#nullable disable
      if (alreadyJoined == null && machineId != 0)
      {
        _db.AuthorizationJoins.Add(new AuthorizationJoin() { MachineId = machineId, EngineerId = engineer.EngineerId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult DeleteMachineAccess(int id, int engineerId)
    {
      AuthorizationJoin join = _db.AuthorizationJoins
      .FirstOrDefault(join => (join.MachineId == id && join.EngineerId == engineerId));
      _db.AuthorizationJoins.Remove(join);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = join.EngineerId });
    }
  }
}