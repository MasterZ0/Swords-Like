using System;
using UnityEngine;
using Z3.GMTK2024.Shared;
using Z3.GMTK2024.States;
using Z3.NodeGraph.Core;
using Z3.Utils.ExtensionMethods;

namespace Z3.GMTK2024
{
    public class AttackPS : CharacterAction
    {
        [SerializeField] private Parameter<Animator> animator;
        [SerializeField] private Parameter<float[]> transitions;

        [ParameterDefinition(AutoBindType.SelfBind)] [SerializeField]
        private Parameter<AnimationEventTrigger> animationEventTrigger;

        [SerializeField] private Parameter<string> eventName;

        private string[] attackAnimationHashed = new[]
        {
            "Attack_01",
            "Attack_02",
            "Attack_03"
        };

        private int currentAttackId = -1;
        private float lastAttackTime = float.NegativeInfinity;
        private bool animationEnded;

        protected override void StartAction()
        {
            base.StartAction();
            animationEnded = false;
            animationEventTrigger.Value.OnEventTrigger += OnEventTrigger;

            DecideAttackId();
            Pawn.CharacterStatus.StartAttack(currentAttackId);

            animator.Value.PlayState(attackAnimationHashed[currentAttackId], transitions.Value[currentAttackId]);
            lastAttackTime = Time.time;
        }

        protected override void UpdateAction()
        {
            if (animationEnded)
            {
                EndAction();
                return;
            }

            base.UpdateAction();
        }

        protected override void StopAction()
        {
            base.StopAction();
            if (!animationEnded)
            {
                // The attack was interrupted before it ends
                // Stop the animation and any damage related logic here
                // Note: the animatedEnded variable can be changed to '!canDealDamage' 
            }

            animationEventTrigger.Value.OnEventTrigger -= OnEventTrigger;
        }

        private void OnEventTrigger(string sentEventName)
        {
            if (!sentEventName.Equals(eventName.Value, StringComparison.OrdinalIgnoreCase))
                return;

            animationEnded = true;
        }

        private void DecideAttackId()
        {
            if (currentAttackId < 0)
            {
                currentAttackId = 0;
                return;
            }

            var timePassedSinceLastAttack = Time.time - lastAttackTime;
            var timeFrame = Data.AttackTimeFrames[currentAttackId];
            timeFrame *= Pawn.Size * Data.SizeData.AttackTimeFrameMultiplier;
            if (timePassedSinceLastAttack < timeFrame)
            {
                currentAttackId++;
                currentAttackId %= attackAnimationHashed.Length;

                return;
            }

            currentAttackId = 0;
        }
    }
}