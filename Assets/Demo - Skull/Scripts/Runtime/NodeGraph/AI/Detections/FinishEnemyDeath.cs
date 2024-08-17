using Z3.DemoSkull.AI;
using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;

namespace Z3.DemoSkull.NodeGraph.AI 
{
    [NodeCategory(Shared.MenuPath.AI)]
    [NodeDescription("Kills given enemy")]
    public class FinishEnemyDeath : ActionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] private Parameter<Enemy> data;

        protected override void StartAction() 
        {
            data.Value.FinishEnemyDeath();
            EndAction();
        }
    }
}