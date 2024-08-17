using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.NodeGraph.Sample.ThirdPerson.Character.States
{
    /// <summary>
    /// Similar than <see cref="MovePS"/>
    /// </summary>
    public class RunPS : CharacterAction
    {
        [SerializeField] private Parameter<float> moveSpeed;
        [SerializeField] private Parameter<float> timeToFullRunning = 0.5f;
        [SerializeField] private Parameter<bool> fullRunning;

        private float timer;

        protected override void StartAction()
        {
            fullRunning.Value = false;
            timer = 0f;
        }

        protected override void UpdateAction()
        {
            timer += DeltaTime;
            Physics.Move(moveSpeed.Value);

            if (timer > timeToFullRunning)
            {
                fullRunning.Value = true;
            }
        }

        protected override void StopAction()
        {
            fullRunning.Value = false;
        }
    }
}