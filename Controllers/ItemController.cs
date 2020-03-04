using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ElecWarehouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElecWarehouse.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ItemController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();

    [HttpGet]
    public List<Item> GetAllItems()
    {
      //Create a GET endpoint for all items in your inventory
      var allItems = db.Items.OrderBy(p => p.Name);
      return allItems.ToList();
    }

    [HttpGet("{id}")]
    public Item GetSingleItem(int id)
    {
      //Create a GET endpoint for each item
      var theItem = db.Items.FirstOrDefault(p => p.Id == id);
      return theItem;
    }

    [HttpPost]
    public Item AddItem(Item item)
    {
      //Create a POST endpoint that allows a client to add an item to the inventory
      db.Items.Add(item);
      db.SaveChanges();
      return item;
    }

    [HttpPut("update_item/{id}")]
    public Item UpdateItem(int id, Item newData)
    {
      //Create a PUT endpoint that allows a client to update an item
      newData.Id = id;
      db.Entry(newData).State = EntityState.Modified;
      db.SaveChanges();
      return newData;
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteItem(int id)
    {
      //Create a DELETE endpoint that allows a client to delete an item
      var toDelete = db.Items.FirstOrDefault(p => p.Id == id);
      if (toDelete == null)
      {
        return NotFound();
      }
      db.Items.Remove(toDelete);
      db.SaveChanges();
      return Ok();
    }

    [HttpGet("out_of_stock")]
    public ActionResult GetOutOfStock()
    {
      //Create a GET endpoint to get all items that are out of stock
      var allGone = db.Items.Where(p => p.NumberInStock < 1);

      return new ContentResult()
      {
        Content = JsonSerializer.Serialize(allGone),
        ContentType = "application/json",
        StatusCode = 200
      };
    }

    [HttpGet("find_item/{SKU}")]
    public ActionResult FindItem(string SKU)
    {
      //Create a GET endpoint to get all items that are out of stock
      var foundItem = db.Items.Where(p => p.SKU == SKU);

      return new ContentResult()
      {
        Content = JsonSerializer.Serialize(foundItem),
        ContentType = "application/json",
        StatusCode = 200
      };
    }


    //Create a GET endpoint that allows them to search for an item based on SKU

  }
}