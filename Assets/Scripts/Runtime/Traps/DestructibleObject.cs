using UnityEngine;
using Z3.Audio.FMODIntegration;
using Z3.GMTK2024.BattleSystem;

namespace Z3
{
    public class DestructibleObject : MonoBehaviour, IStatusOwner
    {
        [SerializeField] private int maxHealth = 1;

        [Header("SFX")] 
        [SerializeField] private SoundData damageSoundReference;
        [SerializeField] private SoundData deathSoundReference;

        [Header("VFX")] 
        [SerializeField] private ParticleSystem deathVFX;


        private IStatusController status;

        public Transform Pivot => transform;
        public Transform Center => transform;

        public IStatusController Status
        {
            get
            {
                if (status != null)
                {
                    return status;
                }

                var basicAttributesController = new BasicAttributesController(maxHealth);
                status = new DestructibleObjectStatus(this, basicAttributesController);

                return status;
            }
        }

        public void OnDamage(DamageInfo damageInfo)
        {
            if (damageSoundReference)
                damageSoundReference.PlaySound(transform);
        }

        public void OnDeath(DamageInfo damageInfo)
        {
            if (deathSoundReference)
                deathSoundReference.PlaySound(transform);

            if (deathVFX)
                Instantiate(deathVFX, transform.position, transform.rotation);

            gameObject.SetActive(false);
        }
    }
}