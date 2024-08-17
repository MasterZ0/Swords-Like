using UnityEngine;
using Z3.NodeGraph.Sample.ThirdPerson.Character.States;

namespace Z3.GMTK2024
{
    public class DashPS : CharacterAction
    {
        private Vector3 dodgeDirection;
        private float time;

        protected override void StartAction()
        {
            Debug.LogError("Getting in");
            time = 0f;
            dodgeDirection = GetDashDirection();
            Physics.SetIgnoreUpdate(true);
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

            Physics.CharacterController.Move(velocityChange);

            if (percentage >= 1f)
            {
                EndAction();
            }
        }

        protected override void StopAction()
        {
            base.StopAction();
            Physics.SetIgnoreUpdate(false);
        }
    }
}