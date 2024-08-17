using Z3.NodeGraph.Core;
using System;
using UnityEngine;

namespace Z3.NodeGraph.Sample.ThirdPerson.Character.States
{
    [NodeDescription("Check the current state of the input")]
    public class CheckControllerPS : CharacterCondition
    {
        public enum ControllerAction
        {
            Move,
            Jump,
            Sprint
        }

        [SerializeField] private Parameter<ControllerAction> controllerAction;

        private Func<bool> getValue;

        public override string Info => $"CheckControllerAction: {controllerAction}";

        public override void StartCondition()
        {
            getValue = () => controllerAction.Value switch
            {
                ControllerAction.Move => Controller.IsMovePressed,
                ControllerAction.Jump => Controller.IsJumpPressed,
                ControllerAction.Sprint => Controller.IsSprintPressed,
                _ => throw new NotImplementedException(),
            };
        }

        public override bool CheckCondition() => getValue();
    }
}