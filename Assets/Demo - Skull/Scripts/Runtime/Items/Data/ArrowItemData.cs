using UnityEngine;
using Z3.DemoSkull.Shared;
using Z3.DemoSkull.Projectiles;
using Z3.DemoSkull.BattleSystem;
using Z3.UIBuilder.Core;

namespace Z3.DemoSkull.Items.Data
{
    [CreateAssetMenu(menuName = MenuPath.Items + "Stack", fileName = "New" + nameof(ArrowItemData))]
    public class ArrowItemData : ItemData, IQuantifiable
    {
        [Title("Arrow")]
        public ArrowProjectile arrowPrefab;

        // Temp
        public DamageData damage;
        public Vector2 velocity;
        public float delay;
    }
}