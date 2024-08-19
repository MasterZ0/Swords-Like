using UnityEngine;
using System;
using Z3.GMTK2024.BattleSystem;
using Z3.GMTK2024.Data;

namespace Z3.GMTK2024
{
    [Serializable]
    public class CharacterStatus : BasicStatusController<BasicAttributesController>
    {
        [Header("Status")]
        [SerializeField] private HitBox swordHitbox;

        protected override bool Invincible => invincibleTime > 0f;

        private CharacterPawn Controller { get; set; }
        private CharacterData Data => Controller.Data;

        private float invincibleTime;

        public void Init(CharacterPawn controller)
        {
            Controller = controller;

            /// Setup Attributes
            Inject(Attributes = new BasicAttributesController(Data.MaxHealth));

            Attributes.SetMaxHP(Data.MaxHealth);
            Attributes.RecoveryAllPoints();

            StartAttack(0);
        }

        public void StartAttack(int comboIndex)
        {
            DamageData damageData = comboIndex switch
            {
                0 => Data.FirstAttackDamage,
                1 => Data.SecondAttackDamage,
                2 => Data.ThirdAttackDamage,
                _ => throw new NotImplementedException(),
            };

            swordHitbox.SetDamage(new Damage(damageData, Controller));
        }

        public void Reset()
        {
            Attributes.SetMaxHP(Data.MaxHealth);
            Attributes.SetHP(Data.MaxHealth);
        }

        public void Update()
        {
            UpdateInvincibility();
        }

        private void UpdateInvincibility()
        {
            if (invincibleTime > 0)
            {
                invincibleTime -= Time.fixedDeltaTime;
                if (invincibleTime <= 0)
                {
                    InvincibilityEnd();
                }
            }
        }

        private void InvincibilityEnd()
        {
            // Finish: Call VFX and layer? (projectile bool)
        }

        protected override void Damage(DamageInfo damageInfo)
        {
            invincibleTime = Data.InvisibleDuration;
            ReceiveDamage(damageInfo);

            // Check shield, damage insity, next state event (criticalInjury), knockback direction
            Controller.SendEvent(CharacterEvent.Injury);
        }

        protected override void Death(DamageInfo damageInfo)
        {
            ReceiveDamage(damageInfo);
            Controller.SendEvent(CharacterEvent.Death);

            Controller.Controller.SetActive(false);
        }


        public void ReceiveDamage(DamageInfo damageInfo)
        {
            /*
            Shaker.RequestShake(damageShakeData);

            StopCoroutine(reddenCoroutine);
            reddenCoroutine = Controller.StartCoroutine(Redden());

            if (!damageInfo.Damage.ShowHitParticle)
                return;

            GetContacts(damageInfo, out Vector3 position, out Quaternion rotation);

            ParticleVFX particleVFX = ObjectPool.SpawnPooledObject(blood, position, rotation);
            particleVFX.SetColor(Settings.BloodColor);*/
        }
    }
}