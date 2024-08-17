using UnityEngine;
using Z3.DemoSkull.Shared;
using Z3.UIBuilder.Core;
using Z3.DemoSkull.BattleSystem;

namespace Z3.DemoSkull.Items.Data
{
    [CreateAssetMenu(menuName = MenuPath.Items + "Weapon", fileName = "New" + nameof(WeaponItemData))]
    public class WeaponItemData : ItemData
    {
        [Title(" Weapon")]
        public DamageData damage;
    }
}