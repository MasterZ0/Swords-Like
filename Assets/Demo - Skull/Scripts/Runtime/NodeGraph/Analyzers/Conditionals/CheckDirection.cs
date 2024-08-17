using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;
using Z3.DemoSkull.Shared;
using Z3.NodeGraph.TaskPack.Utilities;

namespace Z3.DemoSkull.NodeGraph.Analyzers
{

    [NodeCategory(MenuPath.Analyzers)]
    [NodeDescription("Check if the red axis orientation is in the desired direction.")]
    public class CheckDirection : ConditionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] private Parameter<Transform> data;

        [SerializeField] private Parameter<Direction> direction;

        public override string Info => $"Direction == {direction}";

        private const float Min = .5f;
        public override bool CheckCondition()
        {
            return direction.Value switch
            {
                Direction.Left => data.Value.right.x <= -Min,
                Direction.Right => data.Value.right.x >= Min,
                Direction.Up => data.Value.right.y >= Min,
                Direction.Down => data.Value.right.y <= -Min,
                Direction.Forward => data.Value.right.z >= Min,
                Direction.Back => data.Value.right.z <= -Min,
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}