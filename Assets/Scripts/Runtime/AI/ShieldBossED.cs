using System;
using UnityEngine;
using Z3.GMTK2024.BattleSystem;
using Z3.GMTK2024.Shared;

namespace Z3.GMTK2024.AI
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjects + "Shield Boss Data", fileName = "New" + nameof(ShieldBossED))]
    public class ShieldBossED : EnemyData
    {
        [Header("Radius Attack")]
        [Range(0, 100)]
        public int RadiusAttackChance = 30;
        public float RadiusAttackFrequency;
        public Damage radiusAttackDamageSmall;
        public Damage radiusAttackDamageMedium;
        public Damage radiusAttackDamageLarge;

        [Header("Fireball Attack")]
        [Range(0, 100)]
        public int FireballAttackChance = 30;
        public float FireballAttackPreparation = 2f;
        public float FireballAttackProjectileMoveSpeed;
        public float FireballAttackProjectileRotationSpeed;
        public float FireballAttackDuration;
        public float FireballAttackFrequency;
        public float FireballAttackBossRotationSpeed;
        public Damage FireBallDamage;

        [Header("Meteor Attack")]
        [Range(0, 100)]
        public int ThirdAttackChance = 30;
    }
}