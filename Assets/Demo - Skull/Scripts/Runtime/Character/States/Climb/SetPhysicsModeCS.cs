using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.Character.States
{
    /// <summary> Ignore Gravity and Acceleration Speed </summary>
    /// <remarks> Used by Climb, Wall Runs, Dash </remarks>
    public class SetPhysicsModeCS : CharacterAction
    {
        [SerializeField] private Parameter<bool> ignore;

        public override string Info => $"PhysicsMode.Ignore = {ignore}";

        protected override void EnterState()
        {
            Physics.IgnoreUpdate(ignore);
            EndAction();
        }
    }
}