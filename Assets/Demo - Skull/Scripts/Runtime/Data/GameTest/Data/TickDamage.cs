using System;
using UnityEngine;

namespace Z3.DemoSkull.Data
{
    [Serializable]
    public class TickDamage : Effect
    {
        [SerializeField] private int tickDamage = 2;
        [SerializeField] private int tickCount = 5;
    }
}