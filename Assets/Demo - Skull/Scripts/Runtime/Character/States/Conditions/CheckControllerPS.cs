using Z3.NodeGraph.Core;
using System;
using UnityEngine;

namespace Z3.DemoSkull.Character.States
{
    [NodeDescription("Check the current state of the input")]
    public class CheckControllerPS : CharacterCondition
    {
        public enum ControllerAction
        {
            Move,
            Jump,
            Dash,
            PrimarySkill,
            SecondarySkill
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
                ControllerAction.Dash => Controller.IsSprintPressed,
                ControllerAction.PrimarySkill => Controller.IsPrimarySkillPressed,
                ControllerAction.SecondarySkill => Controller.IsSecondarySkillPressed,
                _ => throw new NotImplementedException(),
            };
        }

        public override bool CheckCondition() => getValue();
    }
}