using System;
using System.Collections.Generic;

namespace ElecWarehouse
{

  public class Location
  {

    public int Id { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string ManagerName { get; set; }
    public string PhoneNumber { get; set; }

    public List<LocationItem> LocationItems { get; set; } = new List<LocationItem>();


  }

}