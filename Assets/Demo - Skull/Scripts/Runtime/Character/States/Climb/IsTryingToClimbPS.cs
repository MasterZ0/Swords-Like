using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.Character.States
{
    public class IsTryingToClimbPS : CharacterCondition
    {
        [SerializeField] private Parameter<Vector3> pivotToGrab;
        [SerializeField] private Parameter<Vector3> grabNormal;

        public override bool CheckCondition()
        {
            bool canClimb = Physics.CanClimb();

            if (!canClimb)
                return false;

            bool success = Physics.StartClimb(out Vector3 pivotToGrab, out Vector3 grabNormal);
            if (!success)
                return false;

            this.pivotToGrab.Value = pivotToGrab;
            this.grabNormal.Value = grabNormal;

            return true;
        }
    }
}