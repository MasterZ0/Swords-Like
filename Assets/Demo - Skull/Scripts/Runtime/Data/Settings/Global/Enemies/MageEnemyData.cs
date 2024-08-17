using UnityEngine;
using Z3.DemoSkull.Shared;
using Z3.DemoSkull.BattleSystem;
using Z3.UIBuilder.Core;
using Z3.NodeGraph.TaskPack.AstarPathfinding;

namespace Z3.DemoSkull.Data
{
    [CreateAssetMenu(menuName = MenuPath.SettingsEnemies + "MageSkeleton", fileName = "MageSkeletonSettings")]
    public class MageEnemyData : EnemyData
    {
        [Title("Mage Skeleton Settings")]
        public float idleTime = 2f;
        public float patrolRadius = 5f;
        public float delayToReturnToPatrol = 2f;

        [Header("Movement")]
        public float ikWeightTransition = .8f;
        public float rotationSpeed = 2f;
        public AIPathParameters patrolParameters;
        public AIPathParameters fleeParameters;
        public AIPathParameters chaseParameters;

        [Header("Battle")]
        [Tooltip("Safe distance to re-attack the target")]
        public float chaseDistance = 15f;
        public float escapeDistance = 10f;
        [MinMaxSlider(0f, 40f)]
        public Vector2 offensiveDistanceRange = new Vector2(7f, 12f);
        [Range(0f, 180f)]
        public float angleDifferenceToAttack = 5f;
        public float fireballVelocity = 5f;
        public float delayAfterAttack = 1f;
        public DamageData fireballDamage;
    }
}