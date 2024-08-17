using System;
using System.Collections.Generic;

namespace Z3.DemoSkull.BattleSystem
{
    public abstract class BasicStatusController<TAttributes> : IStatusController where TAttributes : BasicAttributesController
    {
        public event Action<DamageInfo> OnTakeDamage = delegate { };

        public TAttributes Attributes { get; set; }
        IAttributes IStatusController.Attributes => Attributes;
        protected int CurrentHP
        {
            get => Attributes.CurrentHP;
            set => Attributes.SetHP(value);
        }

        protected int CurrentMP
        {
            get => Attributes.CurrentMP;
            set => Attributes.SetMP(value);
        }

        protected int CurrentSP
        {
            get => Attributes.CurrentSP;
            set => Attributes.SetSP(value);
        }

        protected readonly List<SkillEffect> activeEffects = new List<SkillEffect>();
        protected virtual bool Invincible => false;
        private bool IsDead => Attributes.IsDead();

        public void Inject(TAttributes attributes)
        {
            Attributes = attributes;
        }

        public virtual void Update()
        {
            // UpdateEffects
            foreach (SkillEffect effect in activeEffects.ToArray()) // Clone list
            {
                bool finish = effect.Update();
                if (finish)
                {
                    effect.Dispose();
                    activeEffects.Remove(effect);
                }
            }
        }

        public virtual void DamageDealt(DamageInfo damageInfo) => AddHitEffects(damageInfo.SenderEffects);

        public virtual void TakeDamage(Damage damage)
        {
            if (IsDead || Invincible)
                return;

            int effectiveDamage = damage.Value;
            if (CurrentHP - effectiveDamage < 0)
            {
                effectiveDamage = CurrentHP;
            }

            CurrentHP -= effectiveDamage;

            DamageInfo damageInfo = new DamageInfo(damage, this, effectiveDamage);
            OnTakeDamage(damageInfo);

            if (!IsDead)
            {
                AddHitEffects(damageInfo.ReceiverEffects);
                Damage(damageInfo);
            }
            else
            {
                // Remove all effects
                foreach (SkillEffect effect in activeEffects.ToArray())
                {
                    effect.Dispose();
                    activeEffects.Remove(effect);
                }

                Death(damageInfo);
            }
        }

        private void AddHitEffects(List<HitEffect> hitEffects)
        {
            foreach (HitEffect effect in hitEffects)
            {
                activeEffects.Add(effect);
                effect.Start();
            }
        }

        protected abstract void Damage(DamageInfo damageInfo);

        protected abstract void Death(DamageInfo damageInfo);

        public virtual bool Restore(AttributePoint attribute, int amount) => false;
    }
}