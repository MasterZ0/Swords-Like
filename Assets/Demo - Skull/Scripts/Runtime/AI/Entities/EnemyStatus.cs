using Z3.DemoSkull.BattleSystem;

namespace Z3.DemoSkull.AI
{
    public class EnemyStatus : BasicStatusController<BasicAttributesController>
    {
        private readonly Enemy enemy;

        public EnemyStatus(Enemy enemy)
        {
            this.enemy = enemy;
            Inject(new BasicAttributesController());
        }

        public void Reset()
        {
            int maxHp = enemy.EnemyData.MaxHealth;

            Attributes.SetMaxHP(maxHp);
            Attributes.SetHP(maxHp);
        }

        protected override void Damage(DamageInfo damageInfo) => enemy.OnDamage(damageInfo);
        protected override void Death(DamageInfo damageInfo) => enemy.OnDeath(damageInfo);
    }
}
