using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ElecWarehouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/*

Update the PUT endpoint that allows a user/client to update an item for a location = Make a new endpoint for this not an update
Update the PUT endpoint that allows a user/client to update an item for a location
Update the DELETE endpoint that allows a user/client to delete an item for a location
Add a new GET endpoint to get all items that are out of stock for a location. Keep you old GET endpoint for out of stock, but create a new one
Update the GET endpoint that allows the user to search for an item based on SKU, and this should search all the locations.
Deploy your update to Heroku

*/

namespace ElecWarehouse.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ItemController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();

    [HttpGet("locate/{id}")]
    public async Task<ActionResult<List<Item>>> GetAllItems(int id)
    {
      //Create a GET endpoint for all items in your inventory
      var allItems = await db.LocationsItems.Where(l => l.LocationId == id).Select(lItem => lItem.Item).ToListAsync();

      if (allItems == null)
      {
        return NotFound(new { text = "There are no records - " + DateTime.Now });
      }
      else
      {
        return Ok(allItems);
      }
    }

    [HttpGet("{itemId}/{locationID}")]
    //[HttpGet("item_by_location/{itemId, locationID}")]
    public Item GetSingleItem(int itemId, int locationId)
    {
      //Create a GET endpoint for each item
      //var locationItem = db.LocationsItems.FirstOrDefault(i => i.LocationId == locationId && i.ItemId == itemId).Item;
      //var locationItem = db.LocationsItems.FirstOrDefault(i => i.ItemId == itemId).Item;

      //var locationItem = electricCityDb.InventoryItems.Include(item => item.LocationItems.Where(loc => loc.LocationID == locationId && loc.InventoryItemID == id));
      // var locationItem = db.Items.Include(i => i.LocationItems.Where(l => l.LocationId == locationId && l.ItemId == itemId)).FirstOrDefault();
      var item = db.LocationsItems.Include(i => i.Item).Where(i => i.LocationId == locationId && i.ItemId == itemId).Select(s => s.Item).FirstOrDefault();
      return item;
    }



    [HttpPost("{location}/{count}")]
    public async Task<ActionResult<Item>> AddItem(Item item, int location, int count)
    {
      var newInventory = new LocationItem()
      {
        LocationId = location,
        NumberInStock = count
      };

      item.LocationItems.Add(newInventory);

      await db.Items.AddAsync(item);
      await db.SaveChangesAsync();
      item.LocationItems = null;
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