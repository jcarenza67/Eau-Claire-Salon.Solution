using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    private readonly HairSalonContext _db;

    public StylistsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Stylist> model = _db.Stylists.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Stylist stylist)
    {
      try
      {
        if (ModelState.IsValid)
        {
          _db.Stylists.Add(stylist);
          _db.SaveChanges();
          return RedirectToAction("Index");
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError("", "Unable to save changes. Try again");
      }
      return View(stylist);
    }

    public ActionResult Details(int id)
    {
      try
      {
        Stylist thisStylist = _db.Stylists
                                  .Include(stylist => stylist.Clients)
                                  .FirstOrDefault(stylist => stylist.StylistId == id);
        if (thisStylist == null)
        {
          return NotFound();
        }
        return View(thisStylist);
      }
      catch (Exception ex)
      {
        return NotFound();
      }
    }

    public ActionResult Edit(int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      return View(thisStylist);
    }

    [HttpPost]
    public ActionResult Edit(Stylist stylist)
    {
      try
      {
        if (ModelState.IsValid)
        {
          _db.Stylists.Update(stylist);
          _db.SaveChanges();
          return RedirectToAction("Index");
        }
      }
      catch (Exception ex)
      {
        ModelState.AddModelError("", "Unable to save changes. Try again");
      }
      return View(stylist);
    }

    public ActionResult Delete(int id)
    {
      Stylist thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
      return View(thisStylist);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      try
      {
        Stylist thisStylist = _db.Stylists.FirstOrDefault(stylist => stylist.StylistId == id);
        _db.Stylists.Remove(thisStylist);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        return RedirectToAction("Delete", new { id = id, saveChangesError = true });
      }
    }
  }
}