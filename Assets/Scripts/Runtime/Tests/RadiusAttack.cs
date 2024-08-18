using UnityEngine;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using Z3.ObjectPooling;

namespace Z3.GMTK2024
{
    public class RadiusAttack : ActionTask
    {
        [SerializeField] private Parameter<RadiusAttackManager> radiusPrefab;
        [SerializeField] private Parameter<ShieldBossData> bossData;

        protected override void StartAction()
        {
            radiusPrefab.Value = ObjectPool.SpawnPooledObject(radiusPrefab.Value);
            ShieldBossData data = bossData.Value;
            radiusPrefab.Value.SetDamage(data.radiusAttackDamageSmall, data.radiusAttackDamageMedium, data.radiusAttackDamageLarge);
            EndAction();
        }
    }
}
