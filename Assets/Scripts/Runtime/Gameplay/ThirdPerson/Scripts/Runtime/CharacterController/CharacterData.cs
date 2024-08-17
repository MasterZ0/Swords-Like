using UnityEngine;
using UnityEngine.Serialization;
using Z3.NodeGraph.Core;

namespace Z3.GMTK2024.Data
{
    [CreateAssetMenu(menuName = GraphPath.Graph + "Samples/" + nameof(CharacterData),
        fileName = "New" + nameof(CharacterData))]
    public class CharacterData : ScriptableObject
    {
        [Header("General")] [SerializeField] private float groundGravity = 3f;
        [SerializeField] private float mass = 1f;

        [Header(" - Movement")] [SerializeField]
        private float walkMoveSpeed = 2f;

        [SerializeField] private float runMoveSpeed = 6f;
        [SerializeField] private float acceleration = 20f;
        [SerializeField] private float deceleration = 15f;

        [Header(" - Dodge")] [SerializeField] private float dashDuration = 0.7f;
        [SerializeField] private float dashSpeed = 3f;
        [SerializeField] private ParticleSystem.MinMaxCurve dashSpeedVariation;

        [Header(" - Rotation")] [SerializeField]
        private float mouseSensitivity = 0.2f;

        [SerializeField] private float rotationSmoothTime = 0.12f;
        [SerializeField] private Vector2 cameraRangeRotation = new Vector2(-35f, 75f);

        [Header(" - Jump")] [SerializeField] private float jumpVelocity = 1f;
        [SerializeField] private float jumpStopVelocity = 0.15f;
        [SerializeField] private float jumpGravity = 1f;
        [SerializeField] private Vector2 jumpRangeDuration = new Vector2(-0.04f, 0.25f);


        [Header(" - Air")] [SerializeField] private float fallingGravity = 3f;
        [SerializeField] private float maxFallingVelocity = 11f;

        // General
        public float GroundGravity => groundGravity; // Map
        public float Mass => mass;

        // Movement
        public float WalkMoveSpeed => walkMoveSpeed; // Map
        public float RunMoveSpeed => runMoveSpeed; // Map
        public float Acceleration => acceleration;
        public float Deceleration => deceleration;

        // Dodge
        public float DashDuration => dashDuration;

        public float DashSpeed => dashSpeed;
        public ParticleSystem.MinMaxCurve DashSpeedVariation => dashSpeedVariation;

        // Rotation
        public Vector2 CameraRangeRotation => cameraRangeRotation;
        public float RotationSmoothTime => rotationSmoothTime;
        public float MouseSensitivity => mouseSensitivity;

        // Jump
        public float JumpGravity => jumpGravity;
        public float JumpVelocity => jumpVelocity;
        public float JumpStopVelocity => jumpStopVelocity;
        public Vector2 JumpRangeDuration => jumpRangeDuration;

        // Air
        public float FallingGravity => fallingGravity;
        public float MaxFallingVelocity => maxFallingVelocity;
    }
}