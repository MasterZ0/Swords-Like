using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Z3.GMTK2024.BattleSystem;

namespace Z3.GMTK2024
{
    public class ShieldBossData : ScriptableObject
    {

        [Header("General")]
        public float IdleTimeStageA = 2f;
        public float MoveSpeedStageA = 5f;
        public int maxHealth;

        [Header("Radius Attack")]
        public float radiusAttackChance = 40f;
        public float radiusAttackCooldown = 20f;
        public DamageData radiusAttackDamageSmall;
        public DamageData radiusAttackDamageMedium;
        public DamageData radiusAttackDamageLarge;

        [Header("Fire Projectile")]
        public float fireProjectileChance = 20f;
        public DamageData fireProjectileDamage;

    }
}
