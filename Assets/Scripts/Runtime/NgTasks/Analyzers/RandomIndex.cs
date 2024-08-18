using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Z3.GMTK2024.Shared;

namespace Z3.GMTK2024.NgTasks
{
    [NodeCategory(Constants.Analyzers)]
    [NodeDescription("Draws the number index based on the probability of its value.")]
    public class RandomIndex : ActionTask {
                
        [Header("Out")]
        [SerializeField] private Parameter<List<float>> actionChance;
        [SerializeField] private Parameter<int> actionIndex = -1;

        public override string Info => actionChance.Value == null || actionChance.Value.Count == 0 ?
            $"Random Index" :
            $"Random between 0 - {actionChance.Value.Count - 1}";

        protected override void StartAction() {
            List<float> actions = actionChance.Value;

            float maxChance = actions.Sum();
            float random = Random.Range(0, maxChance);
            float counter = 0f;

            for (int i = 0; i < actions.Count; i++) {
                counter += actions[i];

                if (counter >= random) {
                    actionIndex.Value = i;
                    EndAction();
                    return;
                }
            }

            throw new IndexOutOfRangeException();
        }
    }
}