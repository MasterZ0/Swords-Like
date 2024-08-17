using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.Character.States
{
    // Warframe https://www.youtube.com/watch?v=GGIGwnsXIns
    // Legends: https://www.youtube.com/watch?v=DYhwbKLuyx8
    public class VerticalWallRunCS : CharacterAction
    {
        [SerializeField] private Parameter<string> wallRunUp = "WallRun_Up";
        [SerializeField] private Parameter<bool> wallRunInterrupt;

        private float timer;

        protected override void EnterState()
        {

            bool sucess = Physics.GetForwardWallNormal(out Vector3 wallNormal);
            if (!sucess)
            {
                Debug.LogError("VerticalWallRunCS FAIL");
                EndAction();
                return;
            }

            float targetYRotation = Mathf.Atan2(wallNormal.x, wallNormal.z) * Mathf.Rad2Deg + 180f;
            Quaternion targetRotation = Quaternion.Euler(0f, targetYRotation, 0f);

            timer = 0f;
            Physics.SetRotation(targetRotation);

            Animator.PlayAllLayers(wallRunUp);
        }

        protected override void UpdateAction()
        {
            timer += DeltaTime;
            Physics.SetVelocity(Vector3.up * Data.RunMoveSpeed);

            bool movingForward = Controller.Move == Vector2.up;
            if (timer >= Data.VerticalWallRunMaxDuration || !Controller.IsSprintPressed || !movingForward)
            {
                wallRunInterrupt.Value = true;
                EndAction();
                return;
            }

            if (!Physics.CanVerticalWallRun())
            {
                wallRunInterrupt.Value = false;
                EndAction();
            }
        }
    }
}