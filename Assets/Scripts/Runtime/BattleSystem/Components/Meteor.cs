using System.Collections.Generic;
using UnityEngine;
using Z3.ObjectPooling;

namespace Z3.GMTK2024.BattleSystem
{
    public class Meteor : HitBox
    {
        [SerializeField] private HitBox impactFx;
        [SerializeField] private ParticleSystem part;

        private void Reset() => TryGetComponent(out part);

        protected override void OnParticleCollision(GameObject other)
        {
            List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
            int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

            if (numCollisionEvents > 0)
            {
                HitBox instance = ObjectPool.SpawnPooledObject(impactFx, collisionEvents[0].intersection, Quaternion.identity);
                instance.SetDamage(Damage);
            }

            this.ReturnToPool();
        }
    }
}