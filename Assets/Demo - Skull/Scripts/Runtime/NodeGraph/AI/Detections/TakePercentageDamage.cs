using Z3.DemoSkull.BattleSystem;
using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;
using Z3.DemoSkull.Shared;

namespace Z3.DemoSkull.NodeGraph.AI
{
    [NodeCategory(Shared.MenuPath.AI)]
    [NodeDescription("Deals damage to an IDamageable based on a percentage")]
    public class TakePercentageDamage : ActionTask//<IStatusOwner>
    {
        [SerializeField] private Parameter<Transform> sender;
        [SerializeField] private Parameter<float> damagePercentage;

        public override string Info => $"Take {damagePercentage.Value}% Damage";
        
        protected override void StartAction()
        {
            //Agent.TakeDamagePercentage(damagePercentage.Value);
            EndAction();
        }
    }
}