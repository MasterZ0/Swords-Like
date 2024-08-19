using UnityEngine;
using UnityEngine.Animations;
using Z3.GMTK2024.Shared;

namespace Z3.GMTK2024
{
    public class AttackEndedTrigger : StateMachineBehaviour
    {
        [SerializeField] private string eventName = "OnAttackFinished";


        private bool isInitialized;
        private AnimationEventTrigger animationEventTrigger;
        private bool isTriggered;
        private int enterStateCount;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex, controller);
            isTriggered = false;
            enterStateCount++;
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;
            animationEventTrigger = animator.GetComponent<AnimationEventTrigger>();
            animationEventTrigger.OnEventTrigger += OnEventTrigger;
        }

        private void OnEventTrigger(string obj)
        {
            if (obj.Equals(eventName))
            {
                isTriggered = true;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            enterStateCount--;
            if (!isTriggered && enterStateCount == 0)
            {
                animationEventTrigger.OnEventTrigger?.Invoke(eventName);
            }
        }
    }
}