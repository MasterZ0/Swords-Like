using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;
using Z3.DemoSkull.Shared;

namespace Z3.DemoSkull.NodeGraph.Analyzers 
{
    [NodeCategory(MenuPath.Analyzers)]
    [NodeDescription("Randomizes a value from 0 to 100 and returns true based on chance value")]
    public class RandomChance : ConditionTask 
    {
        [SerializeField] private Parameter<float> chance;

        public override string Info => $"{chance}% chance";

        public override bool CheckCondition()
        {
            float random = Random.Range(0f, 100f);
            if (chance.Value == 0)
                return false;

            return chance.Value >= random;
        }
    }
}