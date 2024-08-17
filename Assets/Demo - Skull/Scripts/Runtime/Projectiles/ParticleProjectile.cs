using Z3.ObjectPooling;
using Z3.UIBuilder.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Z3.DemoSkull.Projectiles
{
    public class ParticleProjectile : Projectile
    {
        [Title("Particle Projectile")]
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private UnityEvent onImpact;

        private bool returningToPool;

        private void OnEnable()
        {
            particles.Play();
            rigidbody.detectCollisions = true;
            rigidbody.isKinematic = false;
            returningToPool = false;
        }

        public override void Impact()
        {
            ImpactVFX();

            particles.Stop();
            rigidbody.detectCollisions = false;
            rigidbody.isKinematic = true;
            returningToPool = true;

            onImpact.Invoke();
        }

        private void Update()
        {
            if (returningToPool && !particles.IsAlive(true))
            {
                ObjectPool.ReturnToPool(this);
            }
        }
    }
}
