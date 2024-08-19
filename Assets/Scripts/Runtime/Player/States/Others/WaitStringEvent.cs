using System;
using UnityEngine;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace Z3.GMTK2024.Shared
{
    [NodeCategory(Categories.Events)]
    [NodeDescription("Waits for a graph event")]
    public class WaitStringEvent : ActionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)] [SerializeField]
        private Parameter<AnimationEventTrigger> data;

        [SerializeField] private Parameter<string> eventName;
        private bool actionCalled;

        public override string Info => $"Wait Animation Event [{eventName}]";

        protected override void StartAction()
        {
            actionCalled = false;
            data.Value.OnEventTrigger += OnEventTrigger;
        }

        protected override void StopAction()
        {
            data.Value.OnEventTrigger -= OnEventTrigger;
        }

        private void OnEventTrigger(string sentEventName)
        {
            if (!sentEventName.Equals(eventName.Value, StringComparison.OrdinalIgnoreCase))
                return;

            actionCalled = true;
        }

        protected override void UpdateAction()
        {
            if (actionCalled)
            {
                EndAction();
                return;
            }

            base.UpdateAction();
        }
    }
}