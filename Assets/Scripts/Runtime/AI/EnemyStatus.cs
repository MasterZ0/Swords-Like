using Z3.GMTK2024.BattleSystem;

namespace Z3.GMTK2024.AI
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