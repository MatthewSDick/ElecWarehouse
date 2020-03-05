using System;
using System.Collections.Generic;

namespace ElecWarehouse
{

  public class Item
  {

    public int Id { get; set; }
    public string SKU { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Double Price { get; set; }
    public DateTime DateOrdered { get; set; } = DateTime.Now;
    public List<LocationItem> LocationItems { get; set; } = new List<LocationItem>();

    public int NumberInStock { get; set; }

  }

}