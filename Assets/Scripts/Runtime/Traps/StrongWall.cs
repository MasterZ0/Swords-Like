using UnityEngine;
using Z3.Audio.FMODIntegration;
using Z3.GMTK2024.BattleSystem;

namespace Z3
{
    public class StrongWall : MonoBehaviour, IStatusOwner
    {
        [Header("SFX")] 
        [SerializeField] private SoundData takeHitSFX;
        [SerializeField] private SoundData destroySFX;
        [SerializeField] private Animator animator;
        [SerializeField] private string shakeWallState = "Shake";

        [Header("VFX")] 
        [SerializeField] private ParticleSystem deathVFX;

        public Transform Pivot => transform;
        public Transform Center => transform;

        private IStatusController status;
        public IStatusController Status
        {
            get
            {
                if (status != null)
                    return status;

                BasicAttributesController basicAttributesController = new BasicAttributesController(int.MaxValue);
                status = new WallStatus(this, basicAttributesController);
                return status;
            }
        }

        public void OnDamage(DamageInfo damageInfo)
        {
            if (takeHitSFX)
            {
                takeHitSFX.PlaySound(transform);
            }

            animator.Play(shakeWallState);
        }

        public void Destroy(DamageInfo damageInfo)
        {
            if (destroySFX)
                destroySFX.PlaySound(transform);

            if (deathVFX)
                Instantiate(deathVFX, transform.position, transform.rotation);

            gameObject.SetActive(false);
        }
    }
}