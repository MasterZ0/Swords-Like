using UnityEngine;
using Z3.NodeGraph.Core;
using Z3.Utils;

namespace Z3.DemoSkull.Character.States
{
    public class ClimbStartPS : CharacterAction
    {
        [SerializeField] private Parameter<Vector3> pivotToGrab;
        [SerializeField] private Parameter<Vector3> grabNormal;

        protected override void EnterState()
        {
            Physics.SetVelocity(Vector3.zero);
        }

        protected override void UpdateAction()
        {
            DebugDrawer.DrawSphere(pivotToGrab, .1f);

            // Move to grab point
            Physics.MoveTo(pivotToGrab, Data.ClimbGrabRotationSpeed);

            // Calculate the rotation needed to face the wall
            float angle = Mathf.Atan2(grabNormal.Value.x, grabNormal.Value.z) * Mathf.Rad2Deg + 180f;
            Physics.RotateY(angle, Data.ClimbGrabRotationSpeed);

            float grabDistance = Vector3.Distance(Physics.Transform.position, pivotToGrab);
            float deltaAngleFacingWall = Mathf.DeltaAngle(Physics.Transform.eulerAngles.y, angle);

            if (grabDistance <= 0.01f && deltaAngleFacingWall <= 0.02f)
            {
                // Force rotation
                Physics.SetRotation(Quaternion.Euler(0f, angle, 0f));
                EndAction();
            }
        }
    }
}