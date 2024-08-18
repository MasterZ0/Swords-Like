using UnityEngine;
using Z3.GMTK2024.BattleSystem;
using Z3.NodeGraph.Core;
using Z3.ObjectPooling;

namespace Z3.GMTK2024.NgTasks
{
    public class FireballAttack : BossActionState
    {
        [SerializeField] private Parameter<Transform> transform;
        [SerializeField] private Parameter<Transform> shootPoint;
        [SerializeField] private Parameter<Fireball> fireballPrefab;

        private float nextTimeToAttack;

        protected override void StartAction()
        {
            nextTimeToAttack = 0f;
        }

        protected override void UpdateAction()
        {
            // Rotate boss
            float rotationAmount = ShieldBossData.FireballAttackBossRotationSpeed * DeltaTime;
            transform.Value.rotation = Quaternion.Euler(0, rotationAmount, 0) * transform.Value.rotation;

            // Check if is time to Attack
            nextTimeToAttack -= DeltaTime;
            if (nextTimeToAttack > 0)
                return;

            nextTimeToAttack += ShieldBossData.FireballAttackFrequency;

            // Spawn Fireball
            Fireball fireballInstance = ObjectPool.SpawnPooledObject(fireballPrefab.Value, shootPoint.Value.position, shootPoint.Value.rotation);
            fireballInstance.Shoot(ShieldBossData.FireBallDamage, ShieldBossData.FireballAttackProjectileMoveSpeed, ShieldBossData.FireballAttackProjectileRotationSpeed);
        }
    }
}
