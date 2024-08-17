using UnityEngine;
using System;
using Z3.Utils;

namespace Z3.DemoSkull.Character
{
    /// <summary>
    /// Handles Character physics
    /// </summary>
    [Serializable]
    public sealed class CharacterPhysics : CharacterControllerComponent
    {
        [Header("Layers")]
        [SerializeField] private LayerMask scenaryLayer;

        [Header("Components")]
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private CapsuleCollider physicalBody;
        [SerializeField] private CapsuleCollider bodyTrigger;

        [Header("Points")]
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private Transform interactableCheckPoint;

        [Header("Climb")]
        [SerializeField] private Transform climbDownRaycastPoint;
        [SerializeField] private Transform climbHandContactPoint;

        [Header("Points")]
        [SerializeField] private Transform verticalWallRunPoint;
        [SerializeField] private Transform horizontalWallRunPoint;

        public Vector3 Velocity => rigidbody.velocity;
        public Transform Transform => rigidbody.transform;
        public Rigidbody Rigidbody => rigidbody;
        public CapsuleCollider PhysicalBody => physicalBody;

        private float Weight => Data.Mass * Physics.gravity.y;
        private float EulerYCamera => Controller.Camera.CameraTarget.eulerAngles.y;

        private RaycastHit climbRaycastHit; // Hands point
        private Vector3 velocity;
        private float gravityScale;
        private float moveSpeed;
        private float verticalVelocity;
        private float targetYRotation;
        private float rotationVelocity;

        private bool acceleration;

        private bool ignoreUpdate;

        private float ClimbMaxForwardRaycast // TODO: Review it
        {
            get
            {
                Vector3 point = climbDownRaycastPoint.position;
                point.y = Transform.position.y;
                return (Transform.position - point).magnitude;
            }
        }

        internal bool CheckGround()
        {
            bool hasGround = Physics.CheckSphere(groundCheckPoint.position, Data.GroundCheckRadius, scenaryLayer);
            DebugDrawer.DrawSphere(groundCheckPoint.position, Data.GroundCheckRadius);
            return hasGround;
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
            float rotation = Mathf.SmoothDampAngle(Transform.eulerAngles.y, targetYRotation, ref rotationVelocity, Data.RotationSmoothTime);
            Transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        }

        internal void Update()
        {
            if (!ignoreUpdate)
            {
                UpdateAccelerationSpeed();
                UpdateVerticalVelocity();
            }

            rigidbody.velocity = velocity;
        }

        public void SetVelocity(Vector3 velocity) => this.velocity = velocity;

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

        private void UpdateAccelerationSpeed()
        {
            // Reset state speed
            float speed;

            if (acceleration)
            {
                // Get the Character's speed in a plane
                float currentHorizontalSpeed = new Vector3(Velocity.x, 0f, Velocity.z).magnitude;

                // Get Acceleration or Deceleration transition
                float transition = moveSpeed > currentHorizontalSpeed ? Data.Acceleration : Data.Deceleration;

                // Interpolate between current horizontal speed and target speed
                speed = Mathf.Lerp(currentHorizontalSpeed, moveSpeed, transition * Time.fixedDeltaTime);
                speed = Mathf.Min(moveSpeed, speed);
            }
            else
            {
                speed = moveSpeed;
                acceleration = true;
            }

            moveSpeed = 0f;

            // Update forward velocity
            Vector3 targetDirection = Quaternion.Euler(0f, targetYRotation, 0f) * Vector3.forward;
            velocity = targetDirection.normalized * speed;
        }

        internal void FixedMove(float speed)
        {
            // Movement
            Vector2 direction = Controller.Controller.Move;

            if (direction != Vector2.zero)
            {
                moveSpeed = speed;
                if (direction.magnitude > 1)
                {
                    direction = direction.normalized;
                }
                targetYRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + EulerYCamera;
            }

            // Rotation
            if (!Controller.Camera.YLocked)
            {
                float rotation = Mathf.MoveTowardsAngle(Transform.eulerAngles.y, EulerYCamera, Data.AimBodyCorrectionSpeed);
                Transform.rotation = Quaternion.Euler(0f, rotation, 0f);

                if (MathUtils.AngleDiference(EulerYCamera, Transform.eulerAngles.y) <= Data.FullLockAngle)
                {
                    Controller.Camera.LockY(true);
                }
            }
            else
            {
                float yRotation = Controller.Controller.Look.x + Transform.eulerAngles.y;
                Transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            }
        }
        #endregion

        #region Climb
        public void MoveTo(Vector3 targetPosition, float speed)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, targetPosition, speed * Time.fixedDeltaTime);
            rigidbody.MovePosition(newPosition);
        }

        public void MoveRelative(Vector3 direction, float speed)
        {
            Vector3 newDirection = Transform.TransformDirection(direction.normalized);
            rigidbody.velocity = speed * newDirection;
        }

        public bool CanClimb()
        {
            Vector3 downOrigin = climbDownRaycastPoint.position;
            bool success = PhysicsUtils.RaycastWithSafeOrigin(downOrigin, Vector3.down, out climbRaycastHit, Data.ClimbMaxDownRaycast, scenaryLayer);
            DebugDrawer.DrawRaycast(downOrigin, Vector3.down, climbRaycastHit, Data.ClimbMaxDownRaycast);
            return success;
        }

        public bool CanClimbHorizontal(bool right)
        {
            Vector3 offset = Transform.TransformDirection(right ? Vector3.right : Vector3.left) * Data.ClimbHorizontalDistanceCheck;

            Vector3 forwardOrigin = Transform.position + offset;
            forwardOrigin.y = climbHandContactPoint.position.y - 0.02f;

            float maxForwardDistance = climbDownRaycastPoint.localPosition.z;
            bool hit = PhysicsUtils.RaycastWithSafeOrigin(forwardOrigin, Transform.forward, out RaycastHit forwardHit, maxForwardDistance, scenaryLayer);

            DebugDrawer.DrawRaycast(forwardOrigin, Transform.forward, forwardHit, maxForwardDistance);

            if (!hit)
                return false;

            Vector3 downOrigin = climbDownRaycastPoint.position;
            downOrigin += offset;

            bool success = PhysicsUtils.RaycastWithSafeOrigin(downOrigin, Vector3.down, out RaycastHit downHit, Data.ClimbMaxDownRaycast, scenaryLayer);
            DebugDrawer.DrawRaycast(downOrigin, Vector3.down, downHit, maxForwardDistance);
            return success;
        }

        /// <returns> Grab point and the wall normal </returns>
        public bool StartClimb(out Vector3 grabPoint, out Vector3 wallNormal)
        {
            Vector3 origin = Transform.position;
            origin.y = climbRaycastHit.point.y - 0.01f;

            bool success = PhysicsUtils.RaycastWithSafeOrigin(origin, Transform.forward, out RaycastHit forwardRaycastHit, ClimbMaxForwardRaycast, scenaryLayer);
            if (!success)
            {
                grabPoint = default;
                wallNormal = default;
                return false;
            }

            Vector3 diff = climbHandContactPoint.position - Transform.position;

            grabPoint = forwardRaycastHit.point - diff;
            wallNormal = forwardRaycastHit.normal;

            return true;
        }

        internal void IgnoreUpdate(bool value) => ignoreUpdate = value;

        public void RotateY(float targetAngle, float speed)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            Transform.rotation = Quaternion.Lerp(Transform.rotation, targetRotation, speed * Time.fixedDeltaTime);
        }

        public void SetRotation(Quaternion rotation)
        {
            Transform.rotation = rotation;
        }
        #endregion

        #region Wall Run
        public bool CanHorizontalWallRun()
        {
            bool isObstacleAhead = PhysicsUtils.RaycastWithSafeOrigin(horizontalWallRunPoint.position, Transform.forward, out RaycastHit raycastHit, Data.HorizontalWallRunRaycastDistance, scenaryLayer);
            DebugDrawer.DrawRaycast(horizontalWallRunPoint.position, Transform.forward, raycastHit, Data.HorizontalWallRunRaycastDistance);

            if (isObstacleAhead)
                return false;

            bool isWallRight = CheckWall(Transform.right);
            bool isWallLeft = CheckWall(-Transform.right);

            return isWallRight != isWallLeft;
        }

        private bool CheckWall(Vector3 direction)
        {
            Vector3 foot = Transform.position + Vector3.up * Data.HorizontalWallMinHeight;
            bool isWallDown = PhysicsUtils.RaycastWithSafeOrigin(foot, direction, out RaycastHit raycastHitWallDown, Data.HorizontalWallRunRaycastDistance, scenaryLayer);
            bool isWallUp = PhysicsUtils.RaycastWithSafeOrigin(horizontalWallRunPoint.position, direction, out RaycastHit raycastHitWallUp, Data.HorizontalWallRunRaycastDistance, scenaryLayer);

            DebugDrawer.DrawRaycast(foot, direction, raycastHitWallDown, Data.HorizontalWallRunRaycastDistance);
            DebugDrawer.DrawRaycast(horizontalWallRunPoint.position, direction, raycastHitWallUp, Data.HorizontalWallRunRaycastDistance);

            return isWallUp && isWallDown;
        }

        /// <returns> True = Right, False = Left </returns>
        internal bool StartWallRun()
        {
            return Physics.Raycast(horizontalWallRunPoint.position, Transform.right, out RaycastHit _, Data.HorizontalWallRunRaycastDistance, scenaryLayer);
        }

        public bool CheckWallRun(bool right)
        {
            Vector3 direction = right ? Transform.right : -Transform.right;
            bool isObstacleAhead = PhysicsUtils.RaycastWithSafeOrigin(horizontalWallRunPoint.position, Transform.forward, out RaycastHit raycastHit, Data.HorizontalWallRunRaycastDistance, scenaryLayer);
            
            DebugDrawer.DrawRaycast(horizontalWallRunPoint.position, Transform.forward, raycastHit, Data.HorizontalWallRunRaycastDistance);
            
            return CheckWall(direction) && !isObstacleAhead;
        }

        internal bool GetHorizontalWallNormal(out bool rightWall, out Vector3 wallNormal)
        {
            rightWall = Physics.Raycast(horizontalWallRunPoint.position, Transform.right, out RaycastHit _, Data.HorizontalWallRunRaycastDistance, scenaryLayer);

            Vector3 direction = rightWall ? Transform.right : -Transform.right;
            bool success = Physics.Raycast(horizontalWallRunPoint.position, direction, out RaycastHit raycastHit, Data.HorizontalWallRunRaycastDistance, scenaryLayer);
            wallNormal = raycastHit.normal;
            return success;
        }
        #endregion

        #region Vertical Wall Run
        public bool GetForwardWallNormal(out Vector3 wallNormal)
        {
            Vector3 origin = Transform.position;
            origin.y = verticalWallRunPoint.position.y;
            bool result = Physics.Raycast(origin, Transform.forward, out RaycastHit forwardRaycastHit, physicalBody.radius + 0.1f, scenaryLayer);
            wallNormal = forwardRaycastHit.normal;
            return result;
        }

        public bool CanVerticalWallRun()
        {
            bool success = GetForwardWallNormal(out Vector3 wallNormal);
            if (!success) 
                return false;

            float dotProduct = Vector3.Dot(Transform.forward, -wallNormal); // Vector3.Angle as alternative
            return dotProduct >= Data.MinDotToVerticalWallRun;
        }

        public bool GetForwardWallNormalFoot(out Vector3 wallNormal)
        {
            Vector3 origin = Transform.position;
            bool result = Physics.Raycast(origin, Transform.forward, out RaycastHit forwardRaycastHit, physicalBody.radius + 0.1f, scenaryLayer);
            wallNormal = forwardRaycastHit.point;
            return result;
        }

        /// <summary> Point where the character will jump over </summary>
        public bool GetHurdleCorner(out Vector3 hurdleCornerPoint)
        {
            hurdleCornerPoint = default;
            Vector3 forwardOrigin = Transform.position;
            float maxForwardDistance = ClimbMaxForwardRaycast;
            bool success = Physics.Raycast(forwardOrigin, Transform.forward, out RaycastHit forwardRaycastHit, maxForwardDistance, scenaryLayer);

            DebugDrawer.DrawRaycast(forwardOrigin, Transform.forward, forwardRaycastHit, maxForwardDistance, 2f);

            if (!success)
                return false;

            Vector3 downOrigin = forwardRaycastHit.point + Transform.forward * physicalBody.radius;
            downOrigin.y += physicalBody.height;

            float maxBackDistance = 0.1f + downOrigin.y - forwardRaycastHit.point.y;
            success = Physics.Raycast(downOrigin, Vector3.down, out RaycastHit downRaycastHit, maxBackDistance, scenaryLayer);

            DebugDrawer.DrawRaycast(downOrigin, Vector3.down, downRaycastHit, maxForwardDistance, 1f);

            if (!success)
                return false;

            hurdleCornerPoint = forwardRaycastHit.point;
            hurdleCornerPoint.y = downRaycastHit.point.y;

            return true;
        }
        #endregion

        #region Gizmos
        public void DrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(groundCheckPoint.position, Data.GroundCheckRadius);
            Gizmos.DrawWireSphere(interactableCheckPoint.position, Data.InteractCheckRadius);

            // Climb
            DrawClimbGizmos(Vector3.zero, false);
            DrawClimbGizmos(Vector3.right * Data.ClimbHorizontalDistanceCheck, false);
            DrawClimbGizmos(Vector3.left * Data.ClimbHorizontalDistanceCheck, false);

            // Wall Run
            DrawBothWallRunRaycast(false);
        }

        public void DrawBothWallRunRaycast(bool active = true)
        {
            DrawWallRunSideRaycast(true, active);
            DrawWallRunSideRaycast(false, active);
        }

        /// <summary> Used at run time </summary>
		private void DrawClimbGizmos(Vector3 offset, bool active = true)
        {
            // Rotate the offset
            offset = Transform.TransformDirection(offset);

            // Perform down Raycast
            Vector3 downOrigin = climbDownRaycastPoint.position + offset;

            // Perform forward Raycast
            Vector3 forwardOrigin = Transform.position + offset;
            forwardOrigin.y = climbHandContactPoint.position.y;

            GizmosUtils.DrawRaycast(downOrigin, Vector3.down, Data.ClimbMaxDownRaycast, scenaryLayer, active);
            GizmosUtils.DrawRaycast(forwardOrigin, Transform.forward, climbDownRaycastPoint.localPosition.z, scenaryLayer, active);
        }

        public void DrawWallRunSideRaycast(bool right, bool active = true)
        {
            Vector3 foot = Transform.position + Vector3.up * Data.HorizontalWallMinHeight;

            Vector3 direction = right ? Transform.right : -Transform.right;
            GizmosUtils.DrawRaycast(horizontalWallRunPoint.position, direction, Data.HorizontalWallRunRaycastDistance, scenaryLayer, active);
            GizmosUtils.DrawRaycast(foot, direction, Data.HorizontalWallRunRaycastDistance, scenaryLayer, active);
        }
        #endregion
    }
}