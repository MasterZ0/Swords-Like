using UnityEngine;
using Z3.GMTK2024.States;
using Z3.NodeGraph.Core;
using Z3.Utils.ExtensionMethods;

namespace Z3.GMTK2024
{
    public class AttackPS : CharacterAction
    {
        [SerializeField] private Parameter<Animator> animator;
        [SerializeField] private Parameter<float[]> transitions;

        private string[] attackAnimationHashed = new[]
        {
            "Attack_01",
            "Attack_02",
            "Attack_03"
        };

        private int currentAttackId = -1;
        private float lastAttackTime = float.NegativeInfinity;

        protected override void StartAction()
        {
            base.StartAction();
            DecideAttackId();

            animator.Value.PlayState(attackAnimationHashed[currentAttackId], transitions.Value[currentAttackId]);
            lastAttackTime = Time.time;
            EndAction();
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