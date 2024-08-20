using System;
using System.Collections.Generic;
using Codice.CM.Triggers;
using UnityEngine;
using Z3.Utils.ExtensionMethods;

namespace Z3.GMTK2024
{
    public class TriggerCounter : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;

        private List<Collider> colliders = new List<Collider>();

        public List<Collider> Colliders => colliders;

        private void OnTriggerEnter(Collider other)
        {
            if (!layerMask.CompareLayer(other.gameObject.layer)) return;
            if (other.isTrigger) return;
            colliders.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!layerMask.CompareLayer(other.gameObject.layer)) return;
            if (other.isTrigger) return;
            colliders.Remove(other);
        }
    }
}