using UnityEngine;
using Z3.Audio.WwiseIntegration;
using Z3.GMTK2024.AI;
using Z3.GMTK2024.BattleSystem;

namespace Z3
{
    public class DestructibleObject : MonoBehaviour, IStatusOwner
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int resistent;

        [SerializeField] private Transform center;

        [Header("SFX")] [SerializeField] private SoundData damageSoundReference;
        [SerializeField] private SoundData deathSoundReference;

        [Header("VFX")] [SerializeField] private ParticleSystem deathVFX;


        private IStatusController status;

        public Transform Pivot => transform;
        public Transform Center => center;

        public int Resistent => resistent;

        public IStatusController Status
        {
            get
            {
                if (status != null)
                {
                    return status;
                }

                var basicAttributesController = new BasicAttributesController();
                basicAttributesController.SetMaxHP(maxHealth);
                basicAttributesController.SetHP(maxHealth);
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
            Destroy(gameObject);
        }
    }
}