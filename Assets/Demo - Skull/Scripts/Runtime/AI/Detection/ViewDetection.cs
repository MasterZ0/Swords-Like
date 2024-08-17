using UnityEngine;

namespace Z3.DemoSkull.AI
{
    /// <summary>
    /// Base class to examine the visibility of a target, by raycasting its bounds.
    /// </summary>
    public abstract class ViewDetection : TargetDetection
    {
        [Header("View Detection")]
        [SerializeField] protected LayerMask obstaclesLayer;

        protected bool CanSeeTarget(Collider targetCol)
        {
            if (!targetCol.attachedRigidbody)
                return false;

            foreach (Vector3 edge in GetBoundsEdges(targetCol))
            {
                if (CanSeePosition(edge))
                    return true;
            }

            return false;
        }

        protected Vector3[] GetBoundsEdges(Collider targetCol) // TODO: Improve it
        {
            return new Vector3[3] 
            { 
                targetCol.bounds.center,
                new Vector3(targetCol.bounds.center.x, targetCol.bounds.max.y, targetCol.bounds.center.z),
                new Vector3(targetCol.bounds.center.x, targetCol.bounds.min.y, targetCol.bounds.center.z)
            };
        }

        private bool CanSeePosition(Vector3 target)
        {
            Vector2 direction = target - transform.position;
            float distance = Vector2.Distance(transform.position, target);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstaclesLayer);
            return !hit;
        }
    }
}