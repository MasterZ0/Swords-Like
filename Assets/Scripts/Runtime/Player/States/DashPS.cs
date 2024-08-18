using UnityEngine;
using Z3.GMTK2024.States;

namespace Z3.GMTK2024
{
    public class DashPS : CharacterAction
    {
        private Vector3 dodgeDirection;
        private float time;

        protected override void StartAction()
        {
            time = 0f;
            Physics.SetIgnoreUpdate(true);

            Transform transform = Physics.Transform;
            Vector2 directional = Controller.Move;
            dodgeDirection =  directional != Vector2.zero ? transform.forward : -transform.forward;
        }

        protected override void UpdateAction()
        {
            time += DeltaTime;

            float percentage = time / Data.DashDuration;
            float speedMultiplier = Data.DashSpeedVariation.Evaluate(percentage, Random.Range(0f, 1f));

            Vector3 targetVelocity = dodgeDirection * Data.DashSpeed * speedMultiplier;

            Physics.CharacterController.Move(targetVelocity * DeltaTime);

            if (percentage >= 1f)
            {
                EndAction();
            }
        }

        protected override void StopAction()
        {
            Physics.SetIgnoreUpdate(false);
        }
    }
}