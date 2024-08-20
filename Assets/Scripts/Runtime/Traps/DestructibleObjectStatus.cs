using UnityEngine;
using Z3.GMTK2024.BattleSystem;

namespace Z3
{
    public class DestructibleObjectStatus : BasicStatusController<BasicAttributesController>
    {
        private readonly DestructibleObject destructibleObject;

        public DestructibleObjectStatus(DestructibleObject destructibleObject,
            BasicAttributesController basicAttributesController)
        {
            this.destructibleObject = destructibleObject;
            Inject(basicAttributesController);
        }

        protected override void Damage(DamageInfo damageInfo) => destructibleObject.OnDamage(damageInfo);
        protected override void Death(DamageInfo damageInfo) => destructibleObject.OnDeath(damageInfo);
    }
}