using Z3.NodeGraph.Core;
using UnityEngine;

namespace Z3.GMTK2024.States
{
    public class SetCameraPS : CharacterAction
    {
        [SerializeField] private Parameter<GameObject> newCamera;

        public override string Info => $"{base.Info}: {newCamera}";

        protected override void StartAction()
        {
            Camera.SwitchCamera(newCamera.Value);
            EndAction();
        }
    }
}   