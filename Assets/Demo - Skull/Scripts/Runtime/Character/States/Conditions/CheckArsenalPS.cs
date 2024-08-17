using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.Character.States
{
    [NodeDescription("Used to evaluate the equipped items")]
    public class CheckArsenalPS : CharacterCondition
    {
        public enum ArsenalCondition
        {
            CanShootArrow
        }

        [SerializeField] private Parameter<ArsenalCondition> condition;

        public override bool CheckCondition()
        {
            return true;
            //return condition.value switch
            //{
            //    ArsenalCondition.CanShootArrow => Arsenal.CanShootArrow,
            //    _ => throw new System.NotImplementedException(),
            //};
        }
    }
}