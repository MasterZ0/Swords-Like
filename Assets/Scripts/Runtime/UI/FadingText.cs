using System;
using NUnit.Framework;
using UnityEngine;

namespace Z3.GMTK2024
{
    public class FadingText : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration;

        public bool IsFaded { get; private set; }

        private void Reset()
        {
            canvasGroup = GetComponentInChildren<CanvasGroup>();
        }

        public void ToggleFade()
        {
            IsFaded = !IsFaded;
        }

        private void Update()
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, IsFaded ? 1 : 0, Time.deltaTime * fadeDuration);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                IsFaded = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                IsFaded = false;
        }
    }
}