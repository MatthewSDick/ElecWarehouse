namespace ElecWarehouse
{

  public class OrderItem
  {

    public int ID { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }

  }

}