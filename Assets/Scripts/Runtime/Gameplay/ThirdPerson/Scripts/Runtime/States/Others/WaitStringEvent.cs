using System;
using UnityEngine;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace Z3.NodeGraph.Sample.ThirdPerson.Shared
{
    [NodeCategory(Categories.Events)]
    [NodeDescription("Waits for a graph event")]
    public class WaitStringEvent : ActionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] private Parameter<AnimationEventTrigger> data;
        [SerializeField] private Parameter<string> eventName;

        public override string Info => $"Wait Animation Event [{eventName}]";

        protected override void StartAction()
        {
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

            EndAction();
        }
    }
}