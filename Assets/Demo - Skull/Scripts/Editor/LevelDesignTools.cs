using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using Z3.UIBuilder.Core;

namespace Z3.DemoSkull.Editor
{
    public class LevelDesignTools
    {
        #region Rounder
        [Title("Division Factor")]
        [Range(1, 4)]
        [SerializeField] public int divisionFactor;
        [SerializeField] private Button debugButton;

        [OnInitInspector]
        public void OnCreate()
        {
            debugButton.clicked += () =>
            {
                divisionFactor++;
                Debug.Log(divisionFactor - 1 + " -> " + divisionFactor);
            };
        }

        [Button]
        public void RoundTransform()
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                Undo.RecordObject(obj.transform, "roundedTransform");

                obj.transform.localPosition = Round(obj.transform.localPosition);
            }
        }

        private Vector3 Round(Vector3 inicialValue)
        {
            return new Vector3()
            {
                x = Round(inicialValue.x),
                y = Round(inicialValue.y),
                z = Round(inicialValue.z)
            };
        }

        private float Round(float inicialValue)
        {
            float divisions = (int)Mathf.Pow(divisionFactor, 2f);
            return (float)Math.Round(inicialValue * divisions, MidpointRounding.AwayFromZero) / divisions;
        }
        #endregion
    }
}