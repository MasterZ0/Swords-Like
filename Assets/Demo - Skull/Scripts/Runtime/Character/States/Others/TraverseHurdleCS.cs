using UnityEngine;
using Z3.Utils;
using static UnityEngine.ParticleSystem;

namespace Z3.DemoSkull.Character.States
{
    public class TraverseHurdleCS  : CharacterAction
    {
        [SerializeField] private float duration = .5f;
        [SerializeField] private MinMaxCurve motionXZ;
        [SerializeField] private MinMaxCurve motionY;

        private Vector3 startPosition;
        private Vector3 targetPoint;
        private Vector3 direction;
        private float time;

        protected override void EnterState()
        {
            bool success = Physics.GetHurdleCorner(out Vector3 hurdleCornerPoint);

            if (!success)
            {
                Debug.LogError("ClimbUpPS FAIL");
                EndAction();
                return;
            }

            time = 0f;

            targetPoint = hurdleCornerPoint + Physics.Transform.forward * Physics.PhysicalBody.radius;
            Vector3 difference = targetPoint - Physics.Transform.position;
            float speed = difference.magnitude * duration;

            startPosition = Physics.Transform.position;
            direction = difference.normalized * speed;
        }

        protected override void UpdateAction()
        {
            DebugDrawer.DrawSphere(targetPoint, 0.15f);

            time += DeltaTime;

            float percentage = time / duration;

            float speedMultiplierXZ = motionXZ.Evaluate(percentage);
            float speedMultiplierY = motionY.Evaluate(percentage);

            Vector3 velocity = new()
            {
                x = direction.x * speedMultiplierXZ,
                y = direction.y * speedMultiplierY,
                z = direction.z * speedMultiplierXZ
            };

            Vector3 position = new()
            {
                x = Mathf.LerpUnclamped(startPosition.x, targetPoint.x, speedMultiplierXZ),
                y = Mathf.LerpUnclamped(startPosition.y, targetPoint.y, speedMultiplierY),
                z = Mathf.LerpUnclamped(startPosition.z, targetPoint.z, speedMultiplierXZ),
            };

            Physics.Transform.position = position;
            //Physics.SetVelocity(velocity);

            if (percentage >= 1f)
            {
                EndAction();
            }

            return;
            // -----------------------

            float y = Physics.Transform.position.y;

            if (y <= targetPoint.y)
            {
                Physics.SetVelocity(Vector3.up * Data.ClimbUpSpeed);
                return;
            }

            Vector3 direction2 = Physics.Transform.position - targetPoint;

            Physics.SetVelocity(direction * Data.ClimbUpSpeed);

            Vector3 relativeTargetPoint = Physics.Transform.InverseTransformPoint(targetPoint);
            if (relativeTargetPoint.z <= 0f)
            {
                EndAction();
            }
        }
    }
}