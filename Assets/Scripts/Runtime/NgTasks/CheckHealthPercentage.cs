using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using Z3.UIBuilder.Core;
using Z3.NodeGraph.TaskPack.Utilities;
using UnityEngine;
using Z3.GMTK2024.Shared;
using Z3.GMTK2024.BattleSystem;

namespace Z3.GMTK2024.NgTasks
{
    [NodeCategory(Constants.Category)]
    [NodeDescription("Compare the current health")]
    public class CheckHealthPercentage : ConditionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] private Parameter<IStatusOwner> statusOwner;

        [Slider(0, 100)]
        [SerializeField] private Parameter<float> percentage;
        [SerializeField] private CompareMethod checkType = CompareMethod.LessOrEqualTo;

        public override string Info => $"{statusOwner} HP {checkType.GetString()} {percentage}%";

        public override bool CheckCondition()
        {
            float healthPercentage = statusOwner.Value.GetAttributes().HPPercentage();
            return checkType.Compare(healthPercentage, percentage.Value / 100f);
        }
    }
}