using UnityEngine;
using Z3.GMTK2024.States;
using Z3.Utils.ExtensionMethods;

namespace Z3.GMTK2024
{
    public class DashPS : CharacterAction
    {
        [SerializeField] private string dash = "Dash";
        [SerializeField] private string dodge = "Dodge";
        [SerializeField] private float transition = 0.25f;

        private Vector3 dodgeDirection;
        private float time;

        protected override void StartAction()
        {
            time = 0f;
            // Physics.SetIgnoreUpdate(true);

            Transform transform = Physics.Transform;
            Vector2 directional = Controller.Move;

            if (directional != Vector2.zero)
            {
                dodgeDirection = transform.forward;
                Animator.PlayState(dash, transition);
            }
            else
            {
                dodgeDirection = -transform.forward;
                ;
                Animator.PlayState(dodge, transition);
            }

            Pawn.Status.SetInvincible(Data.DashDuration);
        }

        protected override void UpdateAction()
        {
            time += DeltaTime;

            float percentage = time / Data.DashDuration;
            float speedMultiplier = Data.DashSpeedVariation.Evaluate(percentage, Random.Range(0f, 1f));

            Vector3 targetVelocity = dodgeDirection * Data.DashSpeed * speedMultiplier;

            // Physics.CharacterController.Move(targetVelocity * DeltaTime);
            Physics.ForceMove(targetVelocity);

            if (percentage >= 1f)
            {
                EndAction();
            }
        }

        protected override void StopAction()
        {
            // Physics.SetIgnoreUpdate(false);
        }
    }
}