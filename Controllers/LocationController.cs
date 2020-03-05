using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElecWarehouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElecWarehouse.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LocationController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();
    [HttpGet]
    // A GET endpoint that gets all locations
    public List<Location> GetAllLocations()
    {
      //Create a GET endpoint for all items in your inventory
      var allLocations = db.Locations.OrderBy(l => l.Id);
      return allLocations.ToList();
    }

    [HttpPost]
    //A POST endpoint that allows a user to create a location
    public Location AddLocation(Location Location)
    {
      //Create a POST endpoint that allows a client to add an item to the inventory
      db.Locations.Add(Location);
      db.SaveChanges();
      return Location;
    }



  }
}