using UnityEngine;
using Z3.DemoSkull.BattleSystem;
using Z3.DemoSkull.Shared;
using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.NodeGraph.AI
{
    [NodeCategory(Shared.MenuPath.AI)]
    [NodeDescription("Event that is triggered when the enemy receive a damage.")]
    public class WaitDamage : ActionTask
    {
        [Header("In")]
        [SerializeField] private Parameter<IStatusOwner> statusOwner;

        [Header("Out")]
        [SerializeField] private Parameter<Transform> senderPivot;
        [SerializeField] private Parameter<Transform> senderCenter;
        [SerializeField] private Parameter<Transform> senderHead;

        protected override void StartAction() 
        {
            statusOwner.Value.Status.OnTakeDamage += OnTakeDamage;
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
                senderHead.Value = damageInfo.Sender.Head;
            }

            EndAction();
        }
    }
}