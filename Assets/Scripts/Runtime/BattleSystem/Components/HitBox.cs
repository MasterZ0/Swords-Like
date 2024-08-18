using UnityEngine;

namespace Z3.GMTK2024.BattleSystem
{
    /// <summary>
    /// Applies damage to an IDamageable when triggered
    /// </summary>
    public class HitBox : MonoBehaviour
    {
        protected enum TargetHitType
        {
            Unknown,
            Alive,
            Defeated
        }

        protected Damage Damage { get; private set; }

        public void SetDamage(DamageData damageData, IStatusOwner attacker) => Damage = new Damage(damageData, attacker);
        public void SetDamage(Damage damage) => Damage = damage;

        protected virtual void OnTriggerEnter(Collider collision)
        {
            ApplyDamage(collision);
        }

        protected virtual void OnParticleCollision(GameObject other)
        {
            Collider collision = other.GetComponent<Collider>();
            ApplyDamage(collision);
        }

        protected void ApplyDamage(Collider collision)
        {
            TargetHitType targetHit;
            Vector3 contact = collision.ClosestPoint(transform.position);

            if (collision.attachedRigidbody && collision.attachedRigidbody.TryGetComponent(out IStatusOwner controller))
            {

                Damage.AddHitBoxInfo(this, contact); // TODO: Clone or create a new instance, contact is changing
                controller.TakeDamage(Damage);

                targetHit = controller.IsDead() ? TargetHitType.Defeated : TargetHitType.Alive;
            }
            else
            {
                targetHit = TargetHitType.Unknown;
            }

            AfterHit(targetHit, contact);
        }

        protected virtual void AfterHit(TargetHitType targetHit, Vector3 contact) { }
    }
}
