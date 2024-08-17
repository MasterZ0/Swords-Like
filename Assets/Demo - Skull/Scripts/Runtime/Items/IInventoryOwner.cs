using Z3.DemoSkull.BattleSystem;

namespace Z3.DemoSkull.Items
{
    public interface IInventoryOwner : IBattleEntity
    {
        IInventoryController Inventory { get; }
    }
}