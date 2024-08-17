using UnityEngine;

namespace Z3.DemoSkull.AI
{
    public abstract class TargetDetection : MonoBehaviour
    {
        public bool FindTargetInsideRange(out Transform target)
        {
            target = FindTargetInsideRange()?.transform;
            return target;
        }

        public abstract Rigidbody FindTargetInsideRange();
    }
}