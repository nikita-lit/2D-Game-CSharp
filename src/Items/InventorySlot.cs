namespace Game2D.Items
{
    public class InventorySlot
    {
        public readonly Inventory Inventory;

        public int ID;
        public Item Item;

        public InventorySlot(Inventory inventory, int id)
        {
            Inventory = inventory;
            ID = id;
        }
    }
}
