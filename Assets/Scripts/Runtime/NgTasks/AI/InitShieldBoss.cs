using System.Collections.Generic;
using UnityEngine;
using Z3.GMTK2024.AI;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.TaskPack.Utilities;

namespace Z3.GMTK2024.NgTasks
{
    public class InitShieldBoss : MapParameters<ShieldBossED>
    {
        [SerializeField] private Parameter<List<float>> actionChances;
        [SerializeField] private Parameter<List<int>> actionLimit;

        protected override void StartAction()
        {
            ShieldBossED shieldBoss = data.Value;

            actionChances.Value = new()
            {
                shieldBoss.RadiusAttackChance,
                shieldBoss.FireballAttackChance,
                shieldBoss.ThirdAttackChance,
            };

            actionLimit.Value = new()
            {
                1,
                1,
                1,
            };

            base.StartAction();
        }
    }
}
