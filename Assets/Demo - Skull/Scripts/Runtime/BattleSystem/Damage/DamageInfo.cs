using System.Collections.Generic;

namespace Z3.DemoSkull.BattleSystem
{
    public class DamageInfo
    {
        public Damage Damage { get; set; }
        public IStatusController Receiver { get; set; }
        public int EffectiveDamage { get; set; }

        public List<HitEffect> SenderEffects { get; } // Ex: Heath Steal
        public List<HitEffect> ReceiverEffects { get; } // Ex: Tick Damage
        public bool IsDead => Receiver.IsDead();
        /// <summary> Nullable </summary>
        public IStatusOwner Sender => Damage.Sender;

        public DamageInfo(Damage damage, IStatusController receiver, int effectiveDamage)
        {
            Damage = damage;
            Receiver = receiver;
            EffectiveDamage = effectiveDamage;

            // Effects
            SenderEffects = new List<HitEffect>();
            ReceiverEffects = new List<HitEffect>();

            foreach (HitEffect item in damage.HitEffects)
            {
                if (item.AffectSender)
                {
                    SenderEffects.Add(item);
                }
                if (item.AffectReceiver)
                {
                    ReceiverEffects.Add(item);
                }
            }

            // Call
            Sender?.Status.DamageDealt(this);
        }
    }


}