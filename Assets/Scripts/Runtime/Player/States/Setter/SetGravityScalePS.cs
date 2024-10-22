using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.GMTK2024.States
{
    public class SetGravityScalePS : CharacterAction 
    {
        [SerializeField] private Parameter<float> gravityScale;

        public override string Info => $"Gravity = {gravityScale}";

        protected override void StartAction()
        {
            Physics.SetGravityScale(gravityScale.Value);
            EndAction();
        }
    }
}
