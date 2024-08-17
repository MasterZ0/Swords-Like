using UnityEngine;
using Z3.Utils.ExtensionMethods;

namespace Z3.DemoSkull.AI
{

    //Need to check the angle math later (sometimes the detection works when he is not supposed to. Probably something related
    //to distance.

    /// <summary>
    /// Detects a target within an angle range
    /// </summary>
    public class ViewAngle : ViewDetection
    {
        [Header("View Angle")]
        [SerializeField] private LayerMask targetLayers;
        [SerializeField, Range(0f, 100f)] private float radius = 10;
        [SerializeField, Range(0f, 360f)] private float angle = 100f;

        private Vector3 Center => transform.position;

        public override Rigidbody FindTargetInsideRange()
        {
            //Check if target is within the view range
            Collider[] targets = Physics.OverlapSphere(Center, radius, targetLayers);

            foreach (Collider tempTargetCollider in targets)
            {
                if(!TargetBoundsWithinArc(tempTargetCollider))
                    continue;

                if (CanSeeTarget(tempTargetCollider)) //Check for walls
                {
                    return tempTargetCollider.attachedRigidbody;
                }
            }

            return null;
        }

        private bool TargetBoundsWithinArc(Collider targetCol)
        {
            foreach (Vector3 edge in GetBoundsEdges(targetCol))
            {
                Vector3 directionToCheck = edge - transform.position;
                float targetAngle = Vector3.Angle(transform.forward, directionToCheck);
                if (targetAngle < angle / 2f)
                    return true;
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            Rigidbody rb = FindTargetInsideRange();
            Color arcColor = rb ? new Color(0f, 1f, 0.34f, 0.1f) : new Color(1f, 0.11f, 0.21f, 0.1f);
            Gizmos.color = arcColor;

            if (rb)
                Gizmos.DrawLine(Center, rb.transform.position);

            transform.DrawArc(angle, radius, arcColor);
            transform.DrawWireArc(360f, radius, arcColor);
            Gizmos.DrawLine(Center, Center + transform.forward * radius);
        }
    }
}