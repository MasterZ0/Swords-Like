using Z3.NodeGraph.Core;
using System;
using UnityEngine;

namespace Z3.GMTK2024.States
{
    [NodeDescription(
        "It becomes true when the selected input changes to the desired state, disregarding the current state")]
    public class CheckControllerEventPS : CharacterCondition
    {
        public enum ControllerEvent
        {
            JumpPressed,
            SprintPressed,
            DashPressed,
        }

        [SerializeField] private Parameter<ControllerEvent> controllerEvent;

        private bool actionCalled;

        public override string Info => $"CheckControllerEvent: {controllerEvent}";

        public override void StartCondition() // Enable
        {
            actionCalled = false;
            switch (controllerEvent.Value)
            {
                case ControllerEvent.JumpPressed:
                    Controller.OnJumpPressed += OnCallAction;
                    break;
                case ControllerEvent.SprintPressed:
                    Controller.OnSprintPressed += OnCallAction;
                    break;
                case ControllerEvent.DashPressed:
                    Controller.OnDashPressed += OnCallAction;
                    break;
                default:
                    throw new NotImplementedException(ToString());
            }
        }

        public override void StopCondition() // Disable
        {
            switch (controllerEvent.Value)
            {
                case ControllerEvent.JumpPressed:
                    Controller.OnJumpPressed -= OnCallAction;
                    break;
                case ControllerEvent.SprintPressed:
                    Controller.OnSprintPressed -= OnCallAction;
                    break;
                case ControllerEvent.DashPressed:
                    Controller.OnDashPressed -= OnCallAction;
                    break;
                default:
                    throw new NotImplementedException(ToString());
            }
        }

        private void OnCallAction() => actionCalled = true;

        public override bool CheckCondition()
        {
            bool value = actionCalled;
            actionCalled = false;
            return value;
        }
    }
}