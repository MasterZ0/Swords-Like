using UnityEngine;
using Z3.NodeGraph.Core;
using Z3.Utils.ExtensionMethods;

namespace Z3.NodeGraph.Sample.ThirdPerson.Character.States
{
    public class AirSPS : CharacterAction
    {
        [SerializeField] private Parameter<bool> jumping;
        [SerializeField] private Parameter<string> jumpState = "Jump";
        [SerializeField] private Parameter<string> fallingState = "Falling";

        private bool MinJumpApplied => NodeRunningTime > Data.JumpRangeDuration.x;
        private bool MaxJumpApplied => NodeRunningTime >= Data.JumpRangeDuration.y;
        private bool JumpPressed => Controller.IsJumpPressed;

        private bool falling;

        #region Action
        protected override void StartAction()
        {
            falling = !jumping.Value;

            if (falling)
            {
                Physics.SetGravityScale(Data.FallingGravity);
                Animator.PlayStateAllLayers(fallingState);
            }
            else
            {
                Animator.PlayStateAllLayers(jumpState);
                Physics.SetGravityScale(Data.JumpGravity);
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
                        Physics.Jump(Data.JumpStopVelocity);
                    }
                }
                return;
            }

            // Wait until start fall
            if (!falling && MinJumpApplied && Physics.Velocity.y < 0)
            {
                falling = true;
                Physics.SetGravityScale(Data.FallingGravity);
                Animator.PlayStateAllLayers(fallingState);
            }
        }
        #endregion
    }
}
