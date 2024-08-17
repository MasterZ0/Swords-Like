using UnityEngine;
using Z3.NodeGraph.Core;

namespace Z3.DemoSkull.Character.States
{
    public class CheckCharacterEventPS : CharacterCondition
    {
        [SerializeField] private Parameter<CharacterEvent> eventType;

        private bool actionCalled;
        public override string Info => $"CheckCharacterEvent: {eventType}";

        public override void StartCondition()
        {
            actionCalled = false;
            data.Value.OnCharacterEvent += OnCharacterEvent;
        }

        public override void StopCondition()
        {
            data.Value.OnCharacterEvent -= OnCharacterEvent;
        }

        private void OnCharacterEvent(CharacterEvent playerEvent)
        {
            if (playerEvent == eventType.Value)
            {
                actionCalled = true;
            }
        }

        public override bool CheckCondition()
        {
            bool value = actionCalled;
            actionCalled = false;
            return value;
        }
    }
}