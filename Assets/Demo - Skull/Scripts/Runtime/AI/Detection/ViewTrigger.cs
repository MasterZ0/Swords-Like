using System.Collections.Generic;
using UnityEngine;


namespace Z3.DemoSkull.AI
{
    /// <summary>
    /// Detects a target within a collider component and check if the enemy can see him.
    /// </summary>
    public class ViewTrigger : ViewDetection
    {
        private readonly List<Collider> colsInside = new List<Collider>();

        public override Rigidbody FindTargetInsideRange()
        {
            foreach (Collider colInside in colsInside)
            {
                if (CanSeeTarget(colInside))
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