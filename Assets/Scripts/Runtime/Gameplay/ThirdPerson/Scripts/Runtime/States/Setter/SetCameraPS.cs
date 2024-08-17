using Z3.NodeGraph.Core;
using UnityEngine;

namespace Z3.NodeGraph.Sample.ThirdPerson.Character.States
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