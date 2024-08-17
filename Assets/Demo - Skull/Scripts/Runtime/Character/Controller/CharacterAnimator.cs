using System;
using UnityEngine;

namespace Z3.DemoSkull.Character
{
    [Serializable]
    public sealed class CharacterAnimator : CharacterControllerComponent
    {
        [Header("Character Animator")]
        [SerializeField] private Animator animator;

        [Header("Parameters")]
        [SerializeField] private string velocityXParameter = "VelocityX";
        [SerializeField] private string velocityZParameter = "VelocityZ";

        private float velocityX;
        private float velocityZ;
        private float maxVelocityScale;

        internal void Update()
        {
            // Convert velocity
            Vector3 worldVelocity = Controller.Physics.Velocity;
            Vector3 localVelocity = Controller.Physics.Transform.InverseTransformDirection(worldVelocity);

            // Avoid division by 0
            float maxScale = maxVelocityScale == 0 ? 1 : maxVelocityScale; 
            Vector3 velocityScale = localVelocity / maxScale;

            // Apply smooth
            velocityX = Mathf.MoveTowards(velocityX, velocityScale.x, 1f / Data.AnimationBlendDamp * Time.fixedDeltaTime);
            velocityZ = Mathf.MoveTowards(velocityZ, velocityScale.z, 1f / Data.AnimationBlendDamp * Time.fixedDeltaTime);

            // Round value
            float x = Mathf.Round(velocityX * 10f) / 10f;
            float z = Mathf.Round(velocityZ * 10f) / 10f;

            SetFloat(velocityXParameter, x);
            SetFloat(velocityZParameter, z);
        }

        private void Play(string stateName, float transition = 0.1f, int layerIndex = 0)
        {
            animator.CrossFadeInFixedTime(stateName, transition, layerIndex);
        }

        public void PlayAllLayers(string stateName, float transitTime = 0.25f, int layerCount = 2)
        {
            for (int layerIndex = 0; layerIndex <= layerCount; layerIndex++)
            {
                AnimatorStateInfo current = animator.GetCurrentAnimatorStateInfo(layerIndex);
                animator.CrossFade(stateName, transitTime / current.length, layerIndex);
            }
        }

        private void SetFloat(string parameter, float value) => animator.SetFloat(parameter, value);
    }
}