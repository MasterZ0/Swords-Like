using Z3.ObjectPooling;
using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;
using Z3.DemoSkull.Shared;

namespace Z3.DemoSkull.NodeGraph.Instantiate {

    [NodeCategory(MenuPath.Instantiate)]
    [NodeDescription("Return object to ObjectPool")]
    public class ReturnToPool<T> : ActionTask where T : Component {

        [Header("Return To Pool")]
        [SerializeField] private Parameter<T> prefab;

        public override string Info => $"Return {prefab}";

        protected override void StartAction() {
            prefab.Value.ReturnToPool();
            EndAction();
        }
    }
}