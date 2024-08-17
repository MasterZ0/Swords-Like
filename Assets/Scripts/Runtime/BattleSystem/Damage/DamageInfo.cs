namespace Z3.GMTK2024.BattleSystem
{
    public class DamageInfo
    {
        public Damage Damage { get; set; }
        public IStatusController Receiver { get; set; }
        public int EffectiveDamage { get; set; }
        public bool IsDead => Receiver.IsDead();
        /// <summary> Nullable </summary>
        public IStatusOwner Sender => Damage.Sender;

        public DamageInfo(Damage damage, IStatusController receiver, int effectiveDamage)
        {
            Damage = damage;
            Receiver = receiver;
            EffectiveDamage = effectiveDamage;

            // Call
            Sender?.Status.DamageDealt(this);
        }
    }
}