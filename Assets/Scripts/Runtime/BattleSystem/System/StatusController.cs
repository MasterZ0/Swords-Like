using System;

namespace Z3.GMTK2024.BattleSystem
{
    public abstract class BasicStatusController<TAttributes> : IStatusController where TAttributes : BasicAttributesController
    {
        public event Action OnDeath;
        public event Action<DamageInfo> OnTakeDamage;

        public TAttributes Attributes { get; set; }
        IAttributes IStatusController.Attributes => Attributes;
        protected int CurrentHP
        {
            get => Attributes.CurrentHP;
            set => Attributes.SetHP(value);
        }

        protected virtual bool Invincible => false;
        private bool IsDead => Attributes.IsDead();

        public void Inject(TAttributes attributes)
        {
            Attributes = attributes;
        }

        public virtual void DamageDealt(DamageInfo damageInfo) { }
        public virtual void SetInvincible(float duration)
        {
            // TODO This feels wrong but let's just do this for now
        }

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
            OnTakeDamage?.Invoke(damageInfo);

            if (!IsDead)
            {
                Damage(damageInfo);
            }
            else
            {
                Death(damageInfo);
                OnDeath?.Invoke();
            }
        }

        protected abstract void Damage(DamageInfo damageInfo);

        protected abstract void Death(DamageInfo damageInfo);
    }
}