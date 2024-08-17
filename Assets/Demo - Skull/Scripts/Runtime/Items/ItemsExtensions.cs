namespace Z3.DemoSkull.Items
{
    public static class ItemsExtensions
    {
        public static bool AddItem(this IInventoryOwner owner, ItemReference item)
        {
            return owner.Inventory.AddItem(item);
        }

        public static void AddGold(this IInventoryOwner owner, int amount)
        {
            owner.Inventory.AddGold(amount);
        }
    }
}