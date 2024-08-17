using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Z3.Utils.ExtensionMethods;

namespace Z3.GMTK2024.BattleSystem
{
    public class Damage
    {
        // Damage
        public int Value => damageRange.RandomRange();
        public int StaminaDamage { get; } // Only blockable damages
        // public int Intensity { get; } // Injury?
        public bool ShowHitParticle { get; }
        public DamageType DamageType { get; }
        public IStatusOwner Sender { get; }

        // Hitbox
        public HitBox HitBoxSender { get; private set; }
        public Vector3? ContactPoint { get; private set; }
        public Quaternion? ContactRotation => HitBoxSender ? HitBoxSender.transform.rotation : null;

        private Vector2Int damageRange;

        public Damage(DamageData damageData, IStatusOwner sender)
        {
            StaminaDamage = damageData.value.x;
            damageRange = damageData.value;

            ShowHitParticle = damageData.showHitParticle;

            DamageType = damageData.damageType;
            Sender = sender;
        }

        public Damage(int value)
        {
            damageRange = value.ToVectorInt();
        }

        public void AddHitBoxInfo(HitBox hitBox, Vector3 contact)
        {
            HitBoxSender = hitBox;
            ContactPoint = contact;
        }

        /// <summary> Get Knockback direction </summary>
        public float GetXDirection(Transform transform)
        {
            float targetX = HitBoxSender.transform.position.x - transform.position.x;
            return targetX > 0 ? 1 : -1;
        }
    }
}