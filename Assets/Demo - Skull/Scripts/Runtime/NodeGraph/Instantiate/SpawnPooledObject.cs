using Z3.ObjectPooling;
using Z3.NodeGraph.Tasks;
using Z3.NodeGraph.Core;
using UnityEngine;
using Z3.DemoSkull.Shared;

namespace Z3.DemoSkull.NodeGraph.Instantiate 
{
    [NodeCategory(MenuPath.Instantiate)]
    [NodeDescription("Get object from ObjectPool")]
    public class SpawnPooledObject<T> : ActionTask where T : Component 
    {
        [Header("Spawn Pooled Object")]
        [SerializeField] private Parameter<T> prefab;
        [SerializeField] private Parameter<Vector3> position = Vector3.zero;
        [SerializeField] private Parameter<Quaternion> rotation = Quaternion.identity;
        [SerializeField] private Parameter<Transform> parent = null;

        [Header("Out")]
        [SerializeField] private Parameter<T> returnedObject;

        public override string Info => $"Spawn {prefab}";

        protected override void StartAction() 
        {
            returnedObject.Value = ObjectPool.SpawnPooledObject(prefab.Value, position.Value, rotation.Value, parent.Value);
            EndAction();
        }
    }
}