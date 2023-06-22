namespace BossFight;

public class Item
{
    public string ItemType { get; set; }
    public string ItemName;
    public int Id;
    public int Count;

    public Item(string itemType, string itemName, int id, int count)
    {
        ItemType = itemType;
        ItemName = itemName;
        Id = id;
        Count = count;
    }
}