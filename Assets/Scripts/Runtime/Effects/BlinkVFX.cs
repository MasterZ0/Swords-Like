using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Z3.Effects
{
    public class BlinkVFX : MonoBehaviour
    {
        [SerializeField] private float duration = 0.4f;
        [SerializeField] private Material blinkMaterial;
        [SerializeField] private new Renderer renderer;
        [SerializeField] private List<Renderer> renderers;

        public Renderer Renderer
        {
            get => renderer;
            set
            {
                renderer = value;
                if (value)
                {
                    UpdateInitialMaterial(renderer);
                }
            }
        }

        private readonly Dictionary<Renderer, Material> initialMaterials = new Dictionary<Renderer, Material>();

        private void Awake()
        {
            foreach (Renderer renderer1 in renderers)
            {
                UpdateInitialMaterial(renderer1);
            }
        }

        private void UpdateInitialMaterial(Renderer renderer)
        {
            initialMaterials[renderer] = renderer.material;
        }

        public void Blink()
        {
            if (!renderer) return;

            StopAllCoroutines();
            StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            renderer.material = blinkMaterial;
            foreach (Renderer renderer1 in renderers)
            {
                renderer1.material = blinkMaterial;
            }

            yield return new WaitForSeconds(duration);

            foreach (Renderer renderer1 in renderers)
            {
                renderer1.material = initialMaterials[renderer1];
            }

            renderer.material = initialMaterials[renderer];
        }
    }

    public static class BlinkVFXUtilities
    {
        public static void Blink(this Renderer renderer, float duration)
        {
            var blinkColor = new Color(1f, 1f, 1f, 1f);
            Blink(renderer, duration, blinkColor);
        }

        public static void Blink(this Renderer renderer, float duration, Color blinkColor) { }
    }
}