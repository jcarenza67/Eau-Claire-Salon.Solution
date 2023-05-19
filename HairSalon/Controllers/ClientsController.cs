using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
using System;

  namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    private readonly HairSalonContext _db;

    public ClientsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Client> model = _db.Clients.Include(clients => clients.Stylist).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Client client)
    {
      if (ModelState.IsValid)
      {
        try
        {
          _db.Clients.Add(client);
          _db.SaveChanges();
          return RedirectToAction("Index");
        }
        catch (Exception)
        {
          
          ModelState.AddModelError("", "An error occurred while saving. Please try again.");
        }
      }

      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
      return View(client);
    }

    public ActionResult Details(int id)
    {
      try
      {
        Client thisClient = _db.Clients
                                .Include(client => client.Stylist)      
                                .FirstOrDefault(client => client.ClientId == id);
        if (thisClient == null)
        {
          return NotFound();
        }
        return View(thisClient);
      }
      catch (Exception)
      {
        return NotFound();
      }
    }

    public ActionResult Edit(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
      return View(thisClient);
    }

    [HttpPost]
    public ActionResult Edit(Client client)
    {
      try
      {
        if (ModelState.IsValid)
        {
          _db.Clients.Update(client);
          _db.SaveChanges();
          return RedirectToAction("Index");
        }
      }
      catch (Exception)
      {
        ModelState.AddModelError("", "Unable to save changes. Try again");
      }
      return View(client);
    }

    public ActionResult Delete(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
      return View(thisClient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      try
      {
        Client thisClient = _db.Clients.FirstOrDefault(client => client.ClientId == id);
        _db.Clients.Remove(thisClient);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      catch (Exception)
      {
        return RedirectToAction("Delete", new { id = id, saveChangesError = true });
      }
    }
  }
}