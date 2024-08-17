using UnityEngine;
using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using Z3.DemoSkull.AI;
using Z3.DemoSkull.BattleSystem;
using Z3.DemoSkull.Shared;

namespace Z3.DemoSkull.NodeGraph.AI.General {

    [NodeCategory(Shared.MenuPath.AI)]
    [NodeDescription("Checks if target is inside view.")]
    public class CheckTargetDetection : ConditionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] protected Parameter<TargetDetection> targetDetection;

        [SerializeField] private Parameter<Transform> targetPivot;
        [SerializeField] private Parameter<Transform> targetCenter;
        [SerializeField] private Parameter<Transform> targetHead;
        
        public override bool CheckCondition()
        {
            //The test collisions are all done inside the View classes. The node just return its Transform component.
            if (targetDetection.Value.FindTargetInsideRange(out Transform target))
            {
                if (target.TryGetComponent(out IBattleEntity battleEntity))
                {
                    targetCenter.Value = battleEntity.Center;
                    targetPivot.Value = battleEntity.Pivot;
                    targetHead.Value = battleEntity.Head;
                }
                else
                {
                    targetCenter.Value = target;
                    targetPivot.Value = target;
                    targetHead.Value = target;
                }
                return true;
            }
            return false;
        }
    }
}