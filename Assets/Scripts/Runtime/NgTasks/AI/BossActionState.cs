using UnityEngine;
using Z3.GMTK2024.AI;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace Z3.GMTK2024.NgTasks
{
    public abstract class BossActionState : ActionTask
    {
        [SerializeField] private Parameter<ShieldBossED> shieldBoss;

        public ShieldBossED ShieldBossData { get; private set; }

        protected override void StartAction()
        {
            ShieldBossData = shieldBoss.Value;
        }
    }
}
