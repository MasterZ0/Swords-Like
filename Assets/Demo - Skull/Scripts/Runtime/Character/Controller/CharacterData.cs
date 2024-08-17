using Z3.DemoSkull.Shared;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Z3.DemoSkull.Data
{
    [CreateAssetMenu(menuName = MenuPath.Data + "Character", fileName = "New" + nameof(CharacterData))]
    public class CharacterData : ScriptableObject
    {
        [Header("Physics")]
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private float interactCheckRadius = 0.3f;

        [Header(" - Movement")]
        [SerializeField] private float aimMoveSpeed = 1f;
        [SerializeField] private float walkMoveSpeed = 2f;
        [SerializeField] private float runMoveSpeed = 6f;
        [SerializeField] private float acceleration = 20f;
        [SerializeField] private float deceleration = 15f;

        [Header(" - Rotation")]
        [SerializeField] private float mouseSensitivity = 0.2f;
        [SerializeField] private float rotationSmoothTime = 0.12f;
        [SerializeField] private Vector2 cameraRangeRotation = new Vector2(-35f, 75f);
        [SerializeField] private float aimBodyCorrectionSpeed = 15f;
        [SerializeField] private float fullLockAngle = 15f;

        [Header(" - Jump")]
        [SerializeField] private float jumpVelocity = 1f;
        [SerializeField] private float jumpStopVelocity = 0.15f;
        [SerializeField] private Vector2 jumpRangeDuration = new Vector2(-0.04f, 0.25f);

        [Header(" - Gravity")]
        [SerializeField] private float groundGravity = 3f;
        [SerializeField] private float fallingGravity = 3f;
        [SerializeField] private float jumpGravity = 1f;
        [SerializeField] private float mass = 1f;
        [SerializeField] private float maxFallingVelocity = 11f;

        [Header(" - Visual")]
        [SerializeField] private float animationBlendDamp = 0.1f;


        [Header(" - Dash")]
        [SerializeField] private float dashDuration = 0.1f;
        [SerializeField] private float dashSpeed = 0.1f;
        [SerializeField] private MinMaxCurve dashSpeedVariation;

        [Header(" - Climbing")]
        [Range(0.01f, 2f)]
        [SerializeField] private float climbMaxDownRaycast = 0.5f;
        [Tooltip("Minimum time to climb again")]
        [SerializeField] private float climbCooldown = 0.5f;
        [SerializeField] private float climbUpSpeed = 2f;
        [SerializeField] private float climbHorizontalSpeed = 2.5f;
        [SerializeField] private float climbGrabRotationSpeed = 20f;
        [SerializeField] private float climbHorizontalDistanceCheck = 0.25f;

        [Header(" - Horizontal Wall Run")]
        [SerializeField] private float horizontalWallRunMaxDuration = 2.5f;
        [SerializeField] private float horizontalWallRunRaycastDistance = 0.4f;
        [SerializeField] private float horizontalWallMinHeight = 0.1f;
        [SerializeField] private float horizontalWallRunCooldown = 1f;

        [Header(" - Vertical Wall Run")]
        [SerializeField] private float verticalWallRunRaycastDistance = 1f;
        [SerializeField] private float verticalWallRunMaxDuration = 2f;
        [Range(0.5f, 1f)]
        [SerializeField] private float minDotToVerticalWallRun = .9f;


        public float InteractCheckRadius => interactCheckRadius;
        public float GroundCheckRadius => groundCheckRadius;
        public float Mass => mass;
        public float Acceleration => acceleration;
        public float Deceleration => deceleration;
        public Vector2 CameraRangeRotation => cameraRangeRotation;
        public float RotationSmoothTime => rotationSmoothTime;
        public float MaxFallingVelocity => maxFallingVelocity;

        public float MouseSensitivity => mouseSensitivity;
        public float FallingGravity => fallingGravity;
        public float JumpGravity => jumpGravity;
        public float JumpVelocity => jumpVelocity;
        public float JumpStopVelocity => jumpStopVelocity; // TODO: Review it
        public Vector2 JumpRangeDuration => jumpRangeDuration;

        public float WalkMoveSpeed => walkMoveSpeed;
        public float RunMoveSpeed => runMoveSpeed;
        public float GroundGravity => groundGravity;
        public float AnimationBlendDamp => animationBlendDamp;
        public float AimMoveSpeed => aimMoveSpeed;

        public float AimBodyCorrectionSpeed => aimBodyCorrectionSpeed;
        public float FullLockAngle => fullLockAngle;

        // Dash
        public float DashDuration => dashDuration;
        public float DashSpeed => dashSpeed;
        public MinMaxCurve DashSpeedVariation => dashSpeedVariation;

        // Climb
        public float ClimbUpSpeed => climbUpSpeed;
        public float ClimbHorizontalSpeed => climbHorizontalSpeed;
        public float ClimbGrabRotationSpeed => climbGrabRotationSpeed;
        public float ClimbMaxDownRaycast => climbMaxDownRaycast;
        public float ClimbCooldown => climbCooldown;
        public float ClimbHorizontalDistanceCheck => climbHorizontalDistanceCheck;

        // Horizontal Wall Run
        public float HorizontalWallMinHeight => horizontalWallMinHeight;
        public float HorizontalWallRunRaycastDistance => horizontalWallRunRaycastDistance;
        public float HorizontalWallRunMaxDuration => horizontalWallRunMaxDuration;
        public float HorizontalWallRunCooldown => horizontalWallRunCooldown;

        // Vertical Wall Run
        public float VerticalWallRunRaycastDistance => verticalWallRunRaycastDistance;
        public float VerticalWallRunMaxDuration => verticalWallRunMaxDuration;
        public float MinDotToVerticalWallRun => minDotToVerticalWallRun;
    }
}