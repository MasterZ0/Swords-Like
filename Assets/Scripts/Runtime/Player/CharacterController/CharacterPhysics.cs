using UnityEngine;
using System;
using static UnityEditor.FilePathAttribute;

namespace Z3.GMTK2024
{
    /// <summary>
    /// Handles Character physics
    /// </summary>
    [Serializable]
    public sealed class CharacterPhysics : CharacterControllerComponent
    {
        [Header("Layers")] [SerializeField] private LayerMask scenaryLayer;

        [Header("Components")] [SerializeField]
        private CharacterController characterController;

        [SerializeField] private CapsuleCollider physicalBody;

        public CharacterController CharacterController => characterController;
        public Vector3 Velocity => characterController.velocity;
        public Transform Transform => characterController.transform;
        public CapsuleCollider PhysicalBody => physicalBody;

        private float Weight => Data.Mass * Physics.gravity.y;
        private float EulerYCamera => Controller.Camera.CameraTarget.eulerAngles.y;

        private Vector3 velocity;
        private float gravityScale;
        private float moveSpeed;
        private float verticalVelocity;
        private float targetYRotation;
        private float rotationVelocity;
        private bool isUpdateIgnored;
        private float movementScale = 1;

        internal bool CheckGround() => characterController.isGrounded;

        public float MovementScale
        {
            get => movementScale;
            set => movementScale = value;
        }

        public void Jump(float jumpHeight)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Weight);
        }

        public void SetGravityScale(float gravityScale) => this.gravityScale = gravityScale;

        internal void Move(float speed)
        {
            Vector2 direction = Controller.Controller.Move;

            if (direction == Vector2.zero)
                return;

            moveSpeed = speed;
            if (direction.magnitude > 1)
            {
                direction = direction.normalized;
            }

            targetYRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + EulerYCamera;

            // Rotation
            float rotation = Mathf.SmoothDampAngle(Transform.eulerAngles.y, targetYRotation, ref rotationVelocity,
                Data.RotationSmoothTime);
            Transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        }

        internal void Update()
        {
            if (isUpdateIgnored)
                return;

            UpdateHorizontalVelocity();
            // UpdateVerticalVelocity();
            velocity.y = -Data.MaxFallingVelocity;

            characterController.Move(velocity * Time.fixedDeltaTime);
        }

        public void SetIgnoreUpdate(bool isIgnored)
        {
            isUpdateIgnored = isIgnored;
        }

        #region Private Methods

        /// <summary> Gravity and Jump Velocity </summary>
        private void UpdateVerticalVelocity()
        {
            if (CheckGround() && verticalVelocity < 0f) // Slope force?
            {
                verticalVelocity = -2f;
            }

            float maxFallingVelocity = -Data.MaxFallingVelocity;

            verticalVelocity += Weight * gravityScale * Time.fixedDeltaTime;
            if (verticalVelocity < maxFallingVelocity)
            {
                verticalVelocity = maxFallingVelocity;
            }

            velocity.y = verticalVelocity;
        }

        private void UpdateHorizontalVelocity()
        {
            // Get the Character's speed in a plane
            float currentHorizontalSpeed = new Vector3(Velocity.x, 0f, Velocity.z).magnitude;

            // Get Acceleration or Deceleration transition
            float transition = moveSpeed > currentHorizontalSpeed ? Data.Acceleration : Data.Deceleration;

            // Interpolate between current horizontal speed and target speed
            float speed = Mathf.Lerp(currentHorizontalSpeed, moveSpeed * movementScale,
                transition * Time.fixedDeltaTime * movementScale);
            speed = Mathf.Min(moveSpeed * movementScale, speed);

            moveSpeed = 0f;

            // Update forward velocity
            Vector3 targetDirection = Quaternion.Euler(0f, targetYRotation, 0f) * Vector3.forward;
            velocity = targetDirection.normalized * speed;
        }
        #endregion
    }
}