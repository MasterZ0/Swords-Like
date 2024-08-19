using UnityEngine;
using System;
using Z3.GMTK2024.BattleSystem;
using Z3.GMTK2024.Data;
using System.Collections.Generic;
using System.Linq;

namespace Z3.GMTK2024
{
    [Serializable]
    public class HealthMesh
    {
        [field: SerializeField] public GameObject mesh { get; private set; }
        [Range(0, 1)] [field: SerializeField] public float percentage { get; private set; }
    }

    [Serializable]
    public class CharacterStatus : BasicStatusController<BasicAttributesController>
    {
        [Header("Status")] [SerializeField] private HitBox swordHitbox;
        [SerializeField] private List<HealthMesh> healthMeshes;

        protected override bool Invincible => invincibleTime > 0f;

        private CharacterPawn Controller { get; set; }
        private CharacterData Data => Controller.Data;

        private HealthMesh currentHealthMesh;
        private float invincibleTime;

        public void Init(CharacterPawn controller)
        {
            Controller = controller;

            /// Setup Attributes
            Inject(Attributes = new BasicAttributesController(Data.MaxHealth));

            currentHealthMesh = healthMeshes[0];

            Attributes.OnUpdateStatus += OnUpdateHealth;
            OnUpdateHealth();

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

            int multiplier = Mathf.RoundToInt(Controller.Size * Data.SizeData.DamageSizeMultiplier);
            swordHitbox.SetDamage(new Damage(damageData.value * multiplier, Controller));
        }

        public override void SetInvincible(float duration)
        {
            base.SetInvincible(duration);
            
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

        private void OnUpdateHealth()
        {
            float healthPercentage = Attributes.HPPercentage();

            currentHealthMesh.mesh.SetActive(false);
            // Note: The first element is the higher range and the last must to be 0
            currentHealthMesh = healthMeshes.First(h => healthPercentage >= h.percentage);
            currentHealthMesh.mesh.SetActive(true);
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