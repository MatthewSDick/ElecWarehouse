using System;
using System.Collections.Generic;

namespace ElecWarehouse
{

  public class Order
  {

    public int Id { get; set; }
    public string Name { get; set; }
    public Double Amount { get; set; }
    public DateTime DateOrdered { get; set; } = DateTime.Now;
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

  }

}