using System;
using UnityEngine;
using Z3.GMTK2024.Shared;

namespace Z3.GMTK2024.AI
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjects + "Shield Boss Data", fileName = "New" + nameof(ShieldBossED))]
    public class ShieldBossED : EnemyData
    {
        [Range(0, 100)]
        public int RadiusAttackChance = 30;

        [Range(0, 100)]
        public int FireballAttackChance = 30;

        [Range(0, 100)]
        public int ThirdAttackChance = 30;
    }
}