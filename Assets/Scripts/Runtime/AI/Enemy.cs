using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Z3.UIBuilder.Core;
using Z3.GMTK2024.BattleSystem;
using Z3.Audio.FMODIntegration;
using Z3.Effects;
using Z3.ObjectPooling;
using System.Collections;

namespace Z3.GMTK2024.AI
{
    /// <summary>
    /// Defines enemy basic damage behaviour
    /// </summary>
    public class Enemy : MonoBehaviour, IStatusOwner
    {
        [Title("Enemy")]
        [SerializeField] private EnemyData enemyData;
        [SerializeField] private Transform center;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Image healthBar;

        [Header("Prefabs")]
        [SerializeField] private ParticleVFX hitFX;

        [Header("SFX")]
        [SerializeField] private SoundData damageSoundReference;
        [SerializeField] private SoundData deathSoundReference;

        [SerializeField] private Material hitMaterial;
        [SerializeField] private float hitMaterialSecond;


        [Header("Optional")]
        [SerializeField] private Renderer[] bodyRenderers;
        [SerializeField] private HitBox[] bodyHitBoxes;

        #region Public properties and events
        public event Action OnFinishEnemyDeath = delegate { };

        public EnemyData EnemyData => enemyData;
        public Transform Center => center;
        public Transform Pivot => transform;
        IStatusController IStatusOwner.Status => Status;
        #endregion

        private EnemyStatus status { get; set; }
        public EnemyStatus Status => status ??= new EnemyStatus(this);
        public Transform PlayerTransform => playerTransform;

        private Material[] defaultSharedMaterial;

        #region Initialization
        private void Awake()
        {
            defaultSharedMaterial = bodyRenderers.Select(r => r.sharedMaterial).ToArray();

            Status.Attributes.OnUpdateStatus += OnUpdateStatus;
        }

        private void OnEnable()
        {
            // Reset
            transform.localPosition = Vector2.zero;
            transform.localRotation = Quaternion.identity;

            // Attributes and Status
            Status.Reset();

            // Body damage
            Damage bodyDamage = new Damage(EnemyData.BodyDamage, this);
            foreach (HitBox bodyHitBox in bodyHitBoxes)
            {
                bodyHitBox.SetDamage(bodyDamage);
                bodyHitBox.gameObject.SetActive(true);
            }
        }
        #endregion


        #region Status
        /// <summary> Damage VFX </summary>
        public void OnDamage(DamageInfo damageInfo)
        {
            ApplyHitFX(bodyRenderers, defaultSharedMaterial, hitMaterial, hitMaterialSecond);
            damageSoundReference?.PlaySound(transform);

            GetContacts(damageInfo, out Vector3 position, out Quaternion rotation);

            // Paint Hit Particle
            ParticleVFX spawnedHitParticle = ObjectPool.SpawnPooledObject(hitFX, position, rotation);
        }

        /// <summary> Death VFX </summary>
        public void OnDeath(DamageInfo damageInfo)
        {
            GetContacts(damageInfo, out Vector3 position, out Quaternion rotation);

            deathSoundReference?.PlaySound(transform);
            //ObjectPool.SpawnPooledObject(hitKillFX, position, rotation);

            // Disable components
            bodyHitBoxes.ToList().ForEach(bodyHitBox => bodyHitBox.gameObject.SetActive(false));
        }

        private void GetContacts(DamageInfo damageInfo, out Vector3 contactPoint, out Quaternion contactRotation)
        {
            contactPoint = damageInfo.Damage.ContactPoint ?? transform.position;
            contactRotation = damageInfo.Damage.ContactRotation ?? transform.rotation;
        }
        #endregion

        /// <summary> Disable the enemy after finish the death animation  </summary>
        public void FinishEnemyDeath()
        {
            //ObjectPool.SpawnPooledObject(deathFX, transform.position, transform.rotation);

            OnFinishEnemyDeath.Invoke();
            gameObject.SetActive(false);
        }

        private void OnUpdateStatus()
        {
            healthBar.fillAmount = Status.Attributes.HPPercentage();
        }

        public void ApplyHitFX(Renderer[] renderers, Material[] defaultSharedMaterial, Material hitMaterial, float hitMaterialSeconds)
        {
            StartCoroutine(HitCoroutine());

            IEnumerator HitCoroutine()
            {
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].sharedMaterial = hitMaterial;
                }

                yield return new WaitForSeconds(hitMaterialSeconds);

                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].sharedMaterial = defaultSharedMaterial[i];
                }
            }
        }
    }
}