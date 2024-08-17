using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.Character.States
{
    public class IdlePS : CharacterAction
    {
        [SerializeField] private Parameter<string> idleState;
        [SerializeField] private Parameter<string> overrideIdleState;

        protected override void EnterState()
        {
            string stateName = string.IsNullOrEmpty(overrideIdleState.Value) ? idleState.Value : overrideIdleState.Value;
            Animator.PlayAllLayers(stateName);
            overrideIdleState.Value = string.Empty;
            EndAction();
        }
    }
}