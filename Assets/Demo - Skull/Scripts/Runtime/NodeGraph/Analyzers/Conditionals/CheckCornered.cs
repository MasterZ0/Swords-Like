using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;
using Z3.DemoSkull.Shared;

namespace Z3.DemoSkull.NodeGraph.Analyzers 
{
    [NodeCategory(MenuPath.Analyzers)]
    [NodeDescription("Check for short space between limits and target.")]
    public class CheckCornered : ConditionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] private Parameter<Transform> data;

        [SerializeField] private Parameter<Vector3> target;
        [SerializeField] private Parameter<Vector3> leftLimit;
        [SerializeField] private Parameter<Vector3> rightLimit;
        [SerializeField] private Parameter<float> minimumDistance;

        public override bool CheckCondition()
        {
            if (Mathf.Abs(data.Value.position.x - target.Value.x) < minimumDistance.Value)
            {
                bool targetRight = data.Value.position.x < target.Value.x;

                if (targetRight)    
                {
                    // Cornered on the left
                    return (Mathf.Abs(data.Value.position.x - leftLimit.Value.x) < minimumDistance.Value);
                }
                else
                {
                    // Cornered on the right
                    return (Mathf.Abs(data.Value.position.x - rightLimit.Value.x) < minimumDistance.Value);
                }
            }

            return false;
        }
    }
}