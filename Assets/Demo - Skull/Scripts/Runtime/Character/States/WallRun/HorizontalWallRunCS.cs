using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.Character.States
{
    public class HorizontalWallRunCS : CharacterAction
    {
        [SerializeField] private Parameter<float> lastWallRun;
        [SerializeField] private Parameter<string> wallRunLeft = "WallRun_Left";
        [SerializeField] private Parameter<string> wallRunRight = "WallRun_Right";

        private float duration;
        private bool rightWallRun;

        protected override void EnterState()
        {
            bool success = Physics.GetHorizontalWallNormal(out rightWallRun, out Vector3 wallNormal);
            if (!success)
            {
                Debug.LogError("HorizontalWallRunCS FAIL");
                EndAction();
                return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(wallNormal, Vector3.up);
            targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y + (rightWallRun ? 90f : -90f), 0f);

            duration = 0f;
            Physics.SetRotation(targetRotation);

            if (rightWallRun)
            {
                Animator.PlayAllLayers(wallRunLeft);
            }
            else
            {
                Animator.PlayAllLayers(wallRunRight);
            }
        }

        protected override void UpdateAction()
        {
            duration += DeltaTime;

            bool isRunning = Controller.IsSprintPressed && Controller.IsMovePressed;
            if (!isRunning || duration >= Data.HorizontalWallRunMaxDuration || !Physics.CheckWallRun(rightWallRun))
            {
                EndAction();
                return;
            }

            Physics.SetVelocity(Physics.Transform.forward * Data.RunMoveSpeed);
        }

        protected override void ExitState()
        {
            lastWallRun.Value = Time.time;
        }
    }
}