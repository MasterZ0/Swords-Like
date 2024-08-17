using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.Character.States
{
    public class AirSPS : CharacterAction
    {
        [SerializeField] private Parameter<bool> jumping;

        [SerializeField] private Parameter<float> jumpGravity;
        [SerializeField] private Parameter<float> fallingGravity;
        [SerializeField] private Parameter<string> fallingState = "Falling";
        [SerializeField] private Parameter<string> jumpState = "Jump";

        private bool falling;

        private bool MinJumpApplied => NodeRunningTime > Data.JumpRangeDuration.x;
        private bool MaxJumpApplied => NodeRunningTime >= Data.JumpRangeDuration.y;
        private bool JumpPressed => Controller.IsJumpPressed;

        #region Action
        protected override void EnterState()
        {
            falling = !jumping.Value;

            if (falling)
            {
                Physics.SetGravityScale(fallingGravity.Value);
                Animator.PlayAllLayers(fallingState);
            }
            else
            {
                //VFX.Trail(); Sprint?
                Animator.PlayAllLayers(jumpState);

                //VFX.Jump();
                //SFX.Jump();
                Physics.SetGravityScale(jumpGravity.Value);
            }
        }

        protected override void UpdateAction()
        {
            if (jumping.Value)
            {
                Physics.Jump(Data.JumpVelocity);
                if (MinJumpApplied && (!JumpPressed || MaxJumpApplied))
                {
                    jumping.Value = false;

                    if (!JumpPressed)
                    {
                        // Reset vertical velocity
                        Physics.Jump(Data.JumpStopVelocity); // TODO: Review it
                    }
                }
                return;
            }

            if (!falling && MinJumpApplied && Physics.Velocity.y < 0) // Wait until start fall
            {
                falling = true;
                Physics.SetGravityScale(fallingGravity.Value);
                Animator.PlayAllLayers(fallingState);
            }
        }

        protected override void ExitState()
        {
            //SFX.LandingSoft();
            //VFX.Landing();
            //VFX.SetActiveTrail(false);
        }
        #endregion
    }
}
