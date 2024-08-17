using Z3.DemoSkull.Items.Data;
using System;
using System.Net;

namespace Z3.DemoSkull.Items
{
    public class ItemReference: IEquatable<ItemReference>
    {
        public int amount;
        private string address;
        public ItemData Instance { get; }

        public ItemReference(ItemData itemData, int amount = 1)
        {
            Instance = itemData;
            address = itemData.name;
            this.amount = amount;
        }

        public bool MaxAmount() => amount >= 1000;
        public bool CanStack() => true;
        public int SlotCount() => 1000 - amount;

        public bool Equals(ItemReference other)
        {
            return address == other.address;
        }

        public override string ToString() => address;

        public void Instantiate() { } // TODO: Load in memory

        public void Dispose() { } // TODO: Free memory

        public static implicit operator ItemData(ItemReference reference) => reference.Instance;

        public static implicit operator ItemReference(ItemData instance) => new ItemReference(instance); // Review

        public static bool operator ==(ItemReference a, ItemReference b)
        {
            bool aIsNull = a is null;
            bool bIsNull = b is null;
            if (aIsNull || bIsNull)
            {
                return aIsNull && bIsNull;
            }

            return a.Equals(b);
        }
        public static bool operator !=(ItemReference a, ItemReference b) => !(a == b);

        public static implicit operator bool(ItemReference item) => item is not null;
    }
}