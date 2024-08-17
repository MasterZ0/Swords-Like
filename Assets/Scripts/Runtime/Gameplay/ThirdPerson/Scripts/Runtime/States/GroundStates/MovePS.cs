using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.GMTK2024.States
{
    public class MovePS : CharacterAction 
    {
        [SerializeField] private Parameter<float> moveSpeed;

        protected override void UpdateAction()
        {
            Physics.Move(moveSpeed.Value);
        }
    }
}