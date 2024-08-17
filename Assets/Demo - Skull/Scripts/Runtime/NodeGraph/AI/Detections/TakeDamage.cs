using Z3.DemoSkull.BattleSystem;
using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;

namespace Z3.DemoSkull.NodeGraph.AI
{
    [NodeCategory(Shared.MenuPath.AI)]
    [NodeDescription("Deals damage to IHitable")]
    public class TakeDamage : ActionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] private Parameter<IStatusOwner> data;
        [SerializeField] private Parameter<Transform> sender;
        [SerializeField] private Parameter<int> damageValue;

        public override string Info => $"{base.Info} = {damageValue}";
        
        protected override void StartAction()
        {
            data.Value.TakeDamage(damageValue.Value);
            EndAction();
        }
    }
}