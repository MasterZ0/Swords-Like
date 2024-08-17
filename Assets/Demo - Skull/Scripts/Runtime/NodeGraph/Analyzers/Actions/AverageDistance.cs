using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using System.Collections.Generic;
using UnityEngine;
using Z3.DemoSkull.Shared;
using Z3.NodeGraph.TaskPack.Utilities;

namespace Z3.DemoSkull.NodeGraph.Analyzers
{
    [NodeCategory(MenuPath.Analyzers)]
    [NodeDescription("Filters input indexes considering whether the target is within or outside the average distance")]
    public class AverageDistance : ActionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] private Parameter<Transform> data;
        [Header("In")]
        [SerializeField] private Parameter<List<float>> enter;
        [SerializeField] private Parameter<Vector3> target;

        [Header("Config")]
        public Axis3Flags axis;
        [SerializeField] private Parameter<Vector2> averageDistance;
        public int[] shortRemovedIndex;
        public int[] avarageRemovedIndex;
        public int[] longRemovedIndex;

        [Header("Out")]
        [SerializeField] private Parameter<List<float>> resultObject;

        protected override void StartAction()
        {
            float distance = axis.Distance(data.Value.position, target.Value);
            if (distance < averageDistance.Value.x)
            {
                resultObject.Value = RemoveIndexs(shortRemovedIndex);
            }
            else if (distance > averageDistance.Value.y)
            {
                resultObject.Value = RemoveIndexs(longRemovedIndex);
            }
            else
            {
                resultObject.Value = RemoveIndexs(avarageRemovedIndex);
            }

            EndAction();
        }

        private List<float> RemoveIndexs(int[] removedIndex)
        {

            List<float> result = new List<float>(enter.Value);
            foreach (int index in removedIndex)
            {
                result[index] = 0;
            }
            return result;
        }
    }
}