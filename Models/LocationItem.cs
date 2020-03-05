namespace ElecWarehouse
{

  public class LocationItem
  {

    public int ID { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public int NumberInStock { get; set; }


  }

}