using UnityEngine;
using System.Collections.Generic;

namespace Z3.DemoSkull.AI
{
    public class TriggerDetection : TargetDetection
    {
        private readonly List<Collider> colsInside = new List<Collider>();

        public override Rigidbody FindTargetInsideRange()
        {
            foreach (Collider colInside in colsInside)
            {
                if (colInside.attachedRigidbody)
                {
                    return colInside.attachedRigidbody;
                }
            }
            return null;
        }

        private void OnTriggerEnter(Collider col) => colsInside.Add(col);
        private void OnTriggerExit(Collider col) => colsInside.Remove(col);
    }
}