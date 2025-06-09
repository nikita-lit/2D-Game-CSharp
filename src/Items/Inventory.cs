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
                _slots.Insert(i, new InventorySlot(this, i));
            }
        }

        protected void AttachItem(Item item)
        {
            item.AddFlag(EntityFlag.NoDraw 
                | EntityFlag.NotUsable 
                | EntityFlag.DontCollide);
            item.Parent = Parent;
        }

        protected void DetachItem(Item item)
        {
            item.RemoveFlag(EntityFlag.NoDraw 
                | EntityFlag.NotUsable 
                | EntityFlag.DontCollide);
            item.Parent = null;
            item.Position = Parent.Position + new Vector2(45, 0);
        }

        protected bool StackItem(Item itemA)
        {
            foreach (var slot in _slots)
            {
                var itemB = slot.Item;
                if (itemB != null && itemB.CanStack(itemA))
                {
                    int amountToTransfer = Math.Min(itemA.Stack, itemB.MaxStack - itemB.Stack);
                    itemB.Stack += amountToTransfer;
                    itemA.Stack -= amountToTransfer;

                    if (itemA.Stack <= 0)
                        itemA.Destroy();

                    return true;
                }
            }
            return false;
        }

        public void PickUpItem(Item item)
        {
            if (StackItem(item))
                return;

            for(int i = 0; i < _slots.Count; i++)
            {
                if (_slots[i].Item != null)
                    continue;

                InsertItem(i, item);
                AttachItem(item);
                break;
            }
        }

        protected void InsertItem(int slot, Item item)
        {
            if (!IsSlotExists(slot)) return;
            _slots[slot].Item = item;        
        }

        protected void RemoveItem(int slot)
        {
            if (!IsSlotExists(slot)) return;
            _slots[slot].Item = null;
        }

        public void DropItem(int slot)
        {
            if (!IsSlotExists(slot)) return;

            var item = _slots[slot].Item;
            if (item == null) return;

            if (item.Stack > 1)
            {
                item.Stack--;

                var droppedItem = item.Clone();
                DetachItem(droppedItem);
            }
            else
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
