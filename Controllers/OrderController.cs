using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ElecWarehouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ElecWarehouse.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();



    [HttpGet]
    public List<Order> GetAllOrders() // Validated
    {
      var allItems = db.Orders.OrderBy(l => l.Id);
      return allItems.ToList();
    }

    [HttpGet("test")] // Validated
    public ActionResult GetAllOrdersss()
    {
      return new ContentResult()
      {
        Content = JsonConvert.SerializeObject(db.Orders.Include(n => n.OrderItems).OrderBy(n => n.Id),
        new JsonSerializerSettings
        {
          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }),
        ContentType = "application/json",
        StatusCode = 200
      };
    }

    [HttpPost("new_order")] // Verified
    public async Task<ActionResult<Order>> PlaceOrder(Order order, int item)
    {
      await db.Orders.AddAsync(order);
      await db.SaveChangesAsync();
      order.OrderItems = null;
      return order;
    }

    [HttpPut("update_order/{id}")] // Validated
    public async Task<Order> UpdateOrder(Order order, int id)
    {
      order.Id = id;
      db.Entry(order).State = EntityState.Modified;
      await db.SaveChangesAsync();
      return order;
    }

    [HttpDelete("{id}")] // Verified
    public async Task<ActionResult> DeleteOrder(int id)
    {
      var orderToDelete = db.Orders.FirstOrDefault(i => i.Id == id);

      if (orderToDelete == null)
      {
        return NotFound();
      }

      db.Orders.Remove(orderToDelete);
      await db.SaveChangesAsync();
      return Ok();

    }







  }
}