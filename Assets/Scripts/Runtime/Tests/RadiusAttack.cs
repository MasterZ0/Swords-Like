using UnityEngine;
using Z3.GMTK2024.AI;
using Z3.GMTK2024.NgTasks;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using Z3.ObjectPooling;

namespace Z3.GMTK2024
{
    public class RadiusAttack : BossActionState
    {
        [SerializeField] private Parameter<RadiusAttackManager> radiusPrefab;
        [SerializeField] private Parameter<ShieldBossED> bossData;
        [SerializeField] private Parameter<Transform> stompPoint;

        protected override void StartAction()
        {
            radiusPrefab.Value = ObjectPool.SpawnPooledObject(radiusPrefab.Value);
            ShieldBossED data = bossData.Value;
            radiusPrefab.Value.SetDamage(data.radiusAttackDamageSmall, data.radiusAttackDamageMedium, data.radiusAttackDamageLarge);
            EndAction();
        }

        protected override void UpdateAction()
        {
            // Walk to player? TODO

            // Instantiate Radius Attack
            RadiusAttackManager radiusAttackInstance = ObjectPool.SpawnPooledObject(radiusPrefab.Value, stompPoint.Value.position);
            radiusAttackInstance.SetDamage(ShieldBossData.radiusAttackDamageSmall, ShieldBossData.radiusAttackDamageMedium, ShieldBossData.radiusAttackDamageLarge);
        }
    }
}
