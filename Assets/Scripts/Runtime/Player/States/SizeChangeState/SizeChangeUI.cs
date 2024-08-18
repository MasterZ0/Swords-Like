using System;
using UnityEngine;
using UnityEngine.UI;
using Z3.NodeGraph.Core;

namespace Z3.GMTK2024
{
    public class SizeChangeUI: MonoBehaviour
    {
        [SerializeField] private Image img;

        public void SetPercentage(float percentage)
        {
            percentage = Mathf.Clamp01(percentage);
            img.fillAmount = percentage;
        }
    }
}