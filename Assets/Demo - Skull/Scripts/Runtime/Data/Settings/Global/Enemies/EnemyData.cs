using UnityEngine;
using Z3.DemoSkull.Shared;
using Z3.DemoSkull.BattleSystem;
using System;
using Z3.UIBuilder.Core;

namespace Z3.DemoSkull.Data
{
    public enum Knockback
    {
        Disable,
        Weak,
        Medium,
        Strong
    }

    /// <summary>
    /// Enemy Data
    /// </summary>
    [CreateAssetMenu(menuName = MenuPath.SettingsSub + "Enemy", fileName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [Title("Enemy")]
        [SerializeField] private string enemyName;
        [SerializeField, Range(1f, 10000f)] private int maxHealth = 1;
        [SerializeField] private Gradient hitParticleGradient = new Gradient { colorKeys = new[] { new GradientColorKey(Color.red, 0) } };

        [Header("Drop")]
        [SerializeField] private DropChanceData dropValueData;

        [Header("Body")]
        //[SerializeField] private Knockback knockback;
        [SerializeField] private DamageData bodyDamage;

        public string EnemyName => enemyName;
        public int MaxHealth => maxHealth;
        //public Knockback Knockback => knockback;
        public DropChanceData DropValueData => dropValueData;
        public Gradient HitParticleGradient => hitParticleGradient;
        public DamageData BodyDamage => bodyDamage;

        public event Action OnValueChanged;
        private void OnValidate()
        {
            if (Application.isPlaying)
                OnValueChanged?.Invoke();
        }
    }
}