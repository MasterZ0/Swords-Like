using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;
using Z3.DemoSkull.Shared;
using Z3.NodeGraph.TaskPack.Utilities;

namespace Z3.DemoSkull.NodeGraph.Analyzers
{

    [NodeCategory(MenuPath.Analyzers)]
    [NodeDescription("Compare the Agent position with the target position")]
    public class CheckTargetDirection : ConditionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] private Parameter<Transform> data;

        [SerializeField] private Parameter<Vector3> target;
        [SerializeField] private Parameter<Direction> direction;

        public override string Info => $"Targer Direction == {direction}";

        public override bool CheckCondition()
        {
            return direction.Value switch
            {
                Direction.Left => target.Value.x < data.Value.position.x,
                Direction.Right => target.Value.x > data.Value.position.x,
                Direction.Up => target.Value.y > data.Value.position.y,
                Direction.Down => target.Value.y < data.Value.position.y,
                Direction.Forward => target.Value.z < data.Value.position.z,
                Direction.Back => target.Value.z > data.Value.position.z,
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}