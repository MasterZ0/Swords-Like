namespace Z3.DemoSkull.Items
{
    /// <summary>
    /// Only operations
    /// </summary>
    public interface IInventoryController
    {
        bool AddItem(ItemReference item);
        void AddGold(int amount);
    }
}