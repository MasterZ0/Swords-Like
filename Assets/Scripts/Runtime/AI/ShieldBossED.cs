using System;
using UnityEngine;
using Z3.NodeGraph.TaskPack.AstarPathfinding;
using Z3.GMTK2024.BattleSystem;
using Z3.GMTK2024.Shared;

namespace Z3.GMTK2024.AI
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjects + "Shield Boss Data", fileName = "New" + nameof(ShieldBossED))]
    public class ShieldBossED : EnemyData
    {
        [Header("Shield Boss")]
        public float IdleTime = 2f;

        [Header("- Radius Attack")]
        [Range(0, 100)]
        public int RadiusAttackChance = 30;
        public AIPathParameters RadiusAttackMoveParameters;
        public DamageData radiusAttackDamageSmall;
        public DamageData radiusAttackDamageMedium;
        public DamageData radiusAttackDamageLarge;

        [Header("- Fireball Attack")]
        [Range(0, 100)]
        public int FireballAttackChance = 30;
        public float FireballAttackPreparation = 2f;
        public float FireballAttackProjectileMoveSpeed;
        public float FireballAttackProjectileRotationSpeed;
        public float FireballAttackDuration;
        public float FireballAttackFrequency;
        public float FireballAttackBossRotationSpeed;
        public DamageData FireBallDamage;

        [Header("- Meteor Attack")]
        [Range(0, 100)]
        public int ThirdAttackChance = 30;
        public float MeteorAttackAttackRadius = 18f;
        public float MeteorAttackArenaRadius = 50f;
        public float MeteorAttackFrequency = 0.1f;
        public float MeteorAttackDuration = 10f;
        public float MeteorAttackDelayForBlockRegion = 5f;
        public float MeteorAttackRadiusForBlockRegion = 4f;
        public float RadiusAttackDelayAfter = 3.5f;
        public DamageData MeteorAttackDamage;
    }
}