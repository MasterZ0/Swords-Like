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

            for (int i = 0; i < numCollisionEvents; i++)
            {
                // Instantiate effect at the collision point with an offset
                Vector3 spawnPosition = collisionEvents[i].intersection + collisionEvents[i].normal;
                HitBox instance = ObjectPool.SpawnPooledObject(impactFx, spawnPosition, Quaternion.identity);

                instance.transform.LookAt(transform.position);
                instance.SetDamage(Damage);
            }

            this.ReturnToPool();
        }
    }
}