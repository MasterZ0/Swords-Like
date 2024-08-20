using System;
using System.Collections;
using UnityEngine;
using Z3.GMTK2024;

namespace Z3
{
    public class CharacterPhysicsPusher : MonoBehaviour
    {
        [SerializeField] private float hitForce;
        [SerializeField] private float drag = 1;

        private Vector3 lastFramePosition;
        private Vector3 velocity;

        private void Awake()
        {
            lastFramePosition = transform.position;
        }

        private void Update()
        {
            velocity = transform.position - lastFramePosition;
            lastFramePosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            var rb = other.attachedRigidbody;
            if (rb == null && other is not CharacterController) return;
            var pawn = other.gameObject.GetComponent<CharacterPawn>();
            if (pawn == null) return;
            var physics = pawn.Physics;

            var dir = (other.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(dir, velocity);
            if (dot < 0.1f) return;
            var impact = dir * hitForce;
            impact.y = 0;


            StartCoroutine(MovePlayer());

            IEnumerator MovePlayer()
            {
                while (impact.sqrMagnitude > 0.1f)
                {
                    physics.ForceMove(impact);
                    impact = Vector3.MoveTowards(impact, Vector3.zero, drag * Time.deltaTime);
                    yield return null;
                }
            }
        }
    }
}