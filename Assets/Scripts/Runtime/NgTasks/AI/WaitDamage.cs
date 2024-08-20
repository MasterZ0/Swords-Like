using UnityEngine;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using Z3.GMTK2024.BattleSystem;
using Z3.GMTK2024.Shared;

namespace Z3.GMTK2024.NgTasks
{
    [NodeCategory(Constants.Category)]
    [NodeDescription("Event that is triggered when the enemy receive a damage.")]
    public class WaitDamage : ActionTask
    {
        [Header("In")]
        [SerializeField] private Parameter<IStatusOwner> statusOwner;

        [Header("Out")]
        [SerializeField] private Parameter<Transform> senderPivot;
        [SerializeField] private Parameter<Transform> senderCenter;

        private bool finish;

        protected override void StartAction()
        {
            finish = false;
            statusOwner.Value.Status.OnTakeDamage += OnTakeDamage;
        }

        protected override void UpdateAction()
        {
            if (finish)
            {
                EndAction();
            }
        }

        protected override void StopAction()
        {
            statusOwner.Value.Status.OnTakeDamage -= OnTakeDamage;
        }

        public void OnTakeDamage(DamageInfo damageInfo)
        {
            if (damageInfo.Sender != null)
            {
                senderPivot.Value = damageInfo.Sender.Pivot;
                senderCenter.Value = damageInfo.Sender.Center;
            }

            finish = true;
        }
    }
}
