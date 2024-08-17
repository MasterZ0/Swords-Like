using UnityEngine;
using Z3.DemoSkull.Shared;
using Z3.UIBuilder.Core;
using Z3.DemoSkull.BattleSystem;
using Z3.NodeGraph.TaskPack.AstarPathfinding;

namespace Z3.DemoSkull.Data
{
    [CreateAssetMenu(menuName = MenuPath.SettingsEnemies + "WarriorSkeleton", fileName = "WarriorSkeletonSettings")]
    public class WarriorEnemyData : EnemyData
    {
        [Title("Warrior Skeleton")]
        [SerializeField] private float idleTime = 2f;
        [SerializeField] private float patrolRadius = 5f;
        [SerializeField] private float delayToReturnToPatrol = 2f;

        [Header("Movementation")]
        [SerializeField] private float ikWeightTransition = .8f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] private AIPathParameters battleParameters;
        [SerializeField] private AIPathParameters patrolParameters;

        [Header("Battle")]
        [SerializeField] private float chaseDistance = 15f;
        [SerializeField, Range(0f, 180f)] private float angleDifferenceToAttack = 5f;
        [SerializeField] private float centerAttackAngle = 30f;
        [SerializeField] private float distanceToAttack = 1.4f;
        [SerializeField] private float delayAfterAttack = 1f;
        [SerializeField] private DamageData swordDamage;
        public float IdleTime => idleTime;
        public float PatrolRadius => patrolRadius;
        public float DelayToReturnToPatrol => delayToReturnToPatrol;

        public float IkWeightTransition => ikWeightTransition;
        public float RotationSpeed => rotationSpeed;
        public AIPathParameters BattleParameters => battleParameters;
        public AIPathParameters PatrolParameters => patrolParameters;

        public float ChaseDistance => chaseDistance;
        public float AngleDifferenceToAttack => angleDifferenceToAttack;
        public float CenterAttackAngle => centerAttackAngle;
        public float DistanceToAttack => distanceToAttack;
        public float DelayAfterAttack => delayAfterAttack;
        public DamageData SwordDamage => swordDamage;

        public float PatrolMaxSpeed => patrolParameters.maxSpeed;
        public float BattleMaxSpeed => battleParameters.maxSpeed;
    }
}