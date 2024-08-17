using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.Character.States
{
    public class FixedMovePS : CharacterAction
    {
        [SerializeField] private Parameter<float> moveSpeed;

        protected override void EnterState()
        {
            //Agent.SetSensitivity(SensitivityType.Aim);
            //Animator.SetAimWeight(1f);
        }

        protected override void UpdateAction()
        {
            Physics.FixedMove(moveSpeed.Value);
        }

        protected override void ExitState() 
        {
            Camera.LockY(false);
            //Agent.SetSensitivity(SensitivityType.Default);
            //Animator.SetAimWeight(0f);
        }
    }
}