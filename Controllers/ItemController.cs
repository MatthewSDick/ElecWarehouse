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
    public List<Item> GetEverything()
    {
      var allItems = db.Items.OrderBy(m => m.Name);
      return allItems.ToList();
    }

    [HttpGet("all_by_location/{id}")]
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
    public async Task<ActionResult<Item>> GetSingleItem(int itemId, int locationId)
    {
      var item = await db.LocationsItems.Include(i => i.Item).Where(i => i.LocationId == locationId && i.ItemId == itemId).Select(s => s.Item).FirstOrDefaultAsync();
      if (item == null)
      {
        return NotFound(new { text = "There are no records - " });
      }
      else
      {
        return Ok(item);
      }


    }

    [HttpGet("out_of_stock")] // Works from Heroku
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

    [HttpGet("location_out_of_stock/{location}")]
    public async Task<ActionResult<List<Item>>> LocationGetOutOfStock(int location)
    {
      var allGone = await db.LocationsItems.Include(i => i.Item).Where(l => l.LocationId == location && l.NumberInStock == 0).Select(s => s.Item).FirstOrDefaultAsync();

      if (allGone == null)
      {
        return NotFound(new { text = "There are no records - " + DateTime.Now });
      }
      else
      {
        //return Ok(allGone);
        return new ContentResult()
        {
          Content = JsonSerializer.Serialize(allGone),
          ContentType = "application/json",
          StatusCode = 200
        };
      }









      //return allGone();

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
    public async Task<Item> UpdateItem(int id, Item newData)
    {
      //Create a PUT endpoint that allows a client to update an item
      newData.Id = id;
      db.Entry(newData).State = EntityState.Modified;
      await db.SaveChangesAsync();
      return newData;
    }

    [HttpPut("update_location_item/{id}/{location}")]
    public Item UpdateItem(int location, int id, Item newData)
    {
      var toUpdate = db.LocationsItems.Include(i => i.Item).Where(i => i.LocationId == location && i.ItemId == id).Select(s => s.Item).FirstOrDefault();

      db.Items.AddRange(toUpdate);
      db.SaveChangesAsync();
      return newData;

    }

    [HttpGet("sample/{itemId}/{locationID}")]
    public async Task<ActionResult<Item>> Sample(int itemId, int locationId)
    {
      var item = await db.LocationsItems.Include(i => i.Item).Where(i => i.LocationId == locationId && i.ItemId == itemId).Select(s => s.Item).FirstOrDefaultAsync();
      if (item == null)
      {
        return NotFound(new { text = "There are no records - " });
      }
      else
      {
        return Ok(item);
      }


    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem(int id)
    {
      //Create a DELETE endpoint that allows a client to delete an item
      var toDelete = db.Items.FirstOrDefault(p => p.Id == id);
      if (toDelete == null)
      {
        return NotFound();
      }
      db.Items.Remove(toDelete);
      await db.SaveChangesAsync();
      return Ok();
    }

    [HttpDelete("item_from_location/{id}/{location}")]
    public async Task<ActionResult> DeleteItem(int id, int location)
    {
      //Create a DELETE endpoint that allows a client to delete an item
      var toDelete = db.LocationsItems.FirstOrDefault(i => i.LocationId == location && i.ItemId == id);
      if (toDelete == null)
      {
        return NotFound();
      }
      db.LocationsItems.Remove(toDelete);
      await db.SaveChangesAsync();
      return Ok();
    }


  }
}