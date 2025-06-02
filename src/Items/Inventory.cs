namespace Game2D.Items
{
    public class Inventory
    {
        private List<InventorySlot> _slots = new();
        public IReadOnlyList<InventorySlot> Slots => _slots;

        public Inventory(int slotsCount)
        {
            for(int i = 0; i < slotsCount; i++)
            {
                _slots.Insert(i, new InventorySlot(i));
            }
        }

        public void AttachItem(Item item)
        {
            if (!Contains(item)) return;


        }

        public void PickUpItem(Item item)
        {
            for(int i = 0; i < _slots.Count; i++)
            {
                if (_slots[i].Item != null)
                    continue;

                InsertItem(i, item);
                AttachItem(item);
                break;
            }
        }

        public void InsertItem(int slot, Item item)
        {
            _slots[slot].Item = item;        
        }

        public bool Contains(Item item)
        {
            foreach (var slot in _slots)
            {
                if (slot.Item == item)
                    return true;
            }

            return false;
        }
    }
}
