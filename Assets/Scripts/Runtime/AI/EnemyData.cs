using System;
using UnityEngine;
using Z3.UIBuilder.Core;
using Z3.GMTK2024.BattleSystem;
using Z3.GMTK2024.Shared;

namespace Z3.GMTK2024.AI
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjects + "Enemy Data", fileName = "New" + nameof(EnemyData))]
    public class EnemyData : ScriptableObject
    {
        [Title("Enemy")]
        [SerializeField, Range(1f, 10000f)] private int maxHealth = 1;

        [Header("Body")]
        [SerializeField] private DamageData bodyDamage;

        public int MaxHealth => maxHealth;
        public DamageData BodyDamage => bodyDamage;
    }
}