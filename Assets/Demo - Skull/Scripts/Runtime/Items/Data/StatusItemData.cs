using UnityEngine;
using Z3.DemoSkull.Shared;
using Z3.DemoSkull.BattleSystem;
using Z3.UIBuilder.Core;

namespace Z3.DemoSkull.Items.Data
{
    [CreateAssetMenu(menuName = MenuPath.Items + "Status", fileName = "New" + nameof(StatusItemData))]
    public class StatusItemData : ItemData
    {
        [Title("Status")]

        public AttributePoint attributePoint;
        public int restoreValue;
    }
}