using UnityEngine;
using Z3.ObjectPooling;

namespace Z3.Effects
{
    /// <summary>
    /// Set the particle duration and return to pool when finish.
    /// </summary>
    public class ParticleVFX : MonoBehaviour 
    {
        [SerializeField] protected ParticleSystem particles;

        protected virtual void Reset() => TryGetComponent(out particles);

        public virtual void SetColor(Gradient color)
        {
            ParticleSystem.MainModule mainModule = particles.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        }

        public virtual void SetupParticles(float duration) 
        {
            particles.Stop();
            ParticleSystem.MainModule main = particles.main;
            main.duration = duration;
            particles.Play();
        }

        private void Update()
        {
            if (!particles.IsAlive(true))
            {
                ObjectPool.ReturnToPool(this);
            }
        }
    }
}