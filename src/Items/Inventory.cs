using Game2D.Entities;

namespace Game2D.Items
{
    public class Inventory
    {
        public Entity Parent;
        private List<InventorySlot> _slots = new();
        public IReadOnlyList<InventorySlot> Slots => _slots;

        public Inventory(Entity parent, int slotsCount)
        {
            Parent = parent;
            for (int i = 0; i < slotsCount; i++)
            {
                _slots.Insert(i, new InventorySlot(i));
            }
        }

        public void AttachItem(Item item)
        {
            if (!Contains(item)) return;

            item.AddFlag(EntityFlag.NoDraw 
                | EntityFlag.NotUsable 
                | EntityFlag.DontCollide);
            item.Parent = Parent;
        }

        public void DetachItem(Item item)
        {
            if (!Contains(item)) return;

            item.RemoveFlag(EntityFlag.NoDraw 
                | EntityFlag.NotUsable 
                | EntityFlag.DontCollide);
            item.Parent = null;
            item.Position = Parent.Position + new Vector2(45, 0);
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
            if (!IsSlotExists(slot)) return;
            _slots[slot].Item = item;        
        }

        public void RemoveItem(int slot)
        {
            if (!IsSlotExists(slot)) return;
            _slots[slot].Item = null;
        }

        public void DropItem(int slot)
        {
            if (!IsSlotExists(slot)) return;

            var item = _slots[slot].Item;
            if(item != null)
            {
                DetachItem(item);
                RemoveItem(slot);
            }
        }

        public bool IsSlotExists(int slot) => (slot >= 0 && slot < _slots.Count);

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
