using UnityEngine;
using Z3.Utils;

namespace Z3.DemoSkull.Character.States
{
    public class ClimbMoveCS : CharacterAction
    {
        [SerializeField] private string climbLeftState = "Climb_Left";
        [SerializeField] private string climbRightState = "Climb_Right";

        private bool moveRight;

        protected override void EnterState()
        {
            moveRight = Controller.Move.x > 0f;
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            if (moveRight)
            {
                Animator.PlayAllLayers(climbRightState);
            }
            else
            {
                Animator.PlayAllLayers(climbLeftState);
            }
        }

        protected override void UpdateAction()
        {
            float x = Controller.Move.x;

            // Change direction
            if (moveRight != x > 0)
            {
                moveRight = !moveRight;
                PlayAnimation();
            }

            Vector3 direction = new(x, 0f);
            Physics.MoveRelative(direction, Data.ClimbHorizontalSpeed);
        }

        protected override void ExitState()
        {
            Physics.SetVelocity(Vector3.zero);
        }
    }
}