using UnityEngine;
using Z3.Utils.ExtensionMethods;

namespace Z3.GMTK2024.BattleSystem
{
    public class Damage
    {
        // Damage
        public int Value => damageRange.RandomRange();
        public IStatusOwner Sender { get; }

        // Hitbox
        public HitBox HitBoxSender { get; private set; }
        public Vector3? ContactPoint { get; private set; }
        public Quaternion? ContactRotation => HitBoxSender ? HitBoxSender.transform.rotation : null;

        private Vector2Int damageRange;

        public Damage(DamageData damageData, IStatusOwner sender = null)
        {
            damageRange = damageData.value;
            Sender = sender;
        }

        public Damage(int value, IStatusOwner sender = null)
        {
            damageRange = value.ToVectorInt();
            Sender = sender;
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

        public static implicit operator Damage(DamageData damageData) => new Damage(damageData);
    }
}