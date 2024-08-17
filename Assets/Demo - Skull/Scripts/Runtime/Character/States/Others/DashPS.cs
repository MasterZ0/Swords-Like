using UnityEngine;

namespace Z3.DemoSkull.Character.States
{
    /// <summary>
    /// Similar than Unreal -> Apply Root Motion Constante Force
    /// </summary>
    public class DashPS : CharacterAction
    {
        private Vector3 dodgeDirection;
        private float time;

        protected override void EnterState()
        {
            time = 0f;
            dodgeDirection = GetDashDirection();
        }

        internal Vector3 GetDashDirection()
        {
            // TODO: Camera Lock will be different
            Transform transform = Physics.Transform;

            Vector2 directional = Controller.Move;
            return directional != Vector2.zero ? transform.forward : -transform.forward;
        }

        protected override void UpdateAction()
        {
            time += DeltaTime;

            float percentage = time / Data.DashDuration;
            float speedMultiplier = Data.DashSpeedVariation.Evaluate(percentage, Random.Range(0f, 1f));

            Vector3 targetVelocity = dodgeDirection * Data.DashSpeed * speedMultiplier;
            Vector3 velocityChange = targetVelocity - Physics.Velocity;

            Physics.SetVelocity(velocityChange);
            //Physics.Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            if (percentage >= 1f)
            {
                EndAction();
            }
        }
    }
}