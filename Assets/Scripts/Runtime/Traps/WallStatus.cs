using Z3.GMTK2024.BattleSystem;

namespace Z3
{
    public class WallStatus : BasicStatusController<BasicAttributesController>
    {
        private readonly StrongWall destructibleObject;

        public WallStatus(StrongWall destructibleObject, BasicAttributesController basicAttributesController)
        {
            this.destructibleObject = destructibleObject;
            Inject(basicAttributesController);
        }

        protected override void Damage(DamageInfo damageInfo)
        {
            if (!damageInfo.Damage.StrongAttack)
            {
                destructibleObject.OnDamage(damageInfo);
            }
            else
            {
                destructibleObject.Destroy(damageInfo);
            }
        }

        protected override void Death(DamageInfo damageInfo) => throw new System.NotImplementedException();
    }
}