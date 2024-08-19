using UnityEngine;
using Z3.GMTK2024.NgTasks;
using Z3.NodeGraph.Core;
using Z3.ObjectPooling;

namespace Z3.GMTK2024
{
    public class RadiusAttack : BossActionState
    {
        [SerializeField] private Parameter<RadiusAttackController> radiusPrefab;
        [SerializeField] private Parameter<Transform> stompPoint;

        protected override void StartAction()
        {
            base.StartAction();

            RadiusAttackController newInstance = ObjectPool.SpawnPooledObject(radiusPrefab.Value, stompPoint.Value.position, stompPoint.Value.rotation);
            newInstance.SetDamage(ShieldBossData.radiusAttackDamageSmall, ShieldBossData.radiusAttackDamageMedium, ShieldBossData.radiusAttackDamageLarge);
            EndAction();
        }
    }
}
