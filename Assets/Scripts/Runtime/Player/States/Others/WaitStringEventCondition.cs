using System;
using UnityEngine;
using Z3.GMTK2024.States;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace Z3.GMTK2024.Shared
{
    public class WaitStringEventCondition : ConditionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)] [SerializeField]
        private Parameter<AnimationEventTrigger> data;

        [SerializeField] private Parameter<string> eventName;

        public override string Info => $"Wait Animation Event [{eventName}]";

        private bool actionCalled;

        public override void StartCondition()
        {
            data.Value.OnEventTrigger += OnEventTrigger;
        }

        public override void StopCondition()
        {
            data.Value.OnEventTrigger -= OnEventTrigger;
        }

        private void OnEventTrigger(string sentEventName)
        {
            if (!sentEventName.Equals(eventName.Value, StringComparison.OrdinalIgnoreCase))
                return;

            actionCalled = true;
        }

        public override bool CheckCondition()
        {
            bool value = actionCalled;
            actionCalled = false;
            return value;
        }
    }
}