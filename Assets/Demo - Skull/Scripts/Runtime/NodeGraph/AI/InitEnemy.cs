using Z3.DemoSkull.AI;
using Z3.DemoSkull.Data;
using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;

namespace Z3.DemoSkull.NodeGraph.AI
{
    [NodeCategory(Shared.MenuPath.AI)]
    public class InitEnemy : ActionTask
    {
        [ParameterDefinition(AutoBindType.SelfBind)]
        [SerializeField] private Parameter<Enemy> data;

        private EnemyData EnemyData => data.Value.EnemyData;

        protected override void StartAction()
        {
            //SetParameters();

            EnemyData.OnValueChanged += OnDataChanged;

            EndAction();
        }

        private void OnDataChanged()
        {
            if (data.Value && data.Value.gameObject.activeSelf)//ownerSystemBlackboard
            {
                //SetParameters();
            }
            else
            {
                EnemyData.OnValueChanged -= OnDataChanged;  
            }
        }
    }
}