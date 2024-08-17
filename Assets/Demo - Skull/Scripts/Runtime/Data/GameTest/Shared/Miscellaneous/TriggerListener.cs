using UnityEngine;
using UnityEngine.Events;

namespace Z3.DemoSkull
{
    /// <summary>
    /// Easy way to check a trigger
    /// </summary>
    public class TriggerListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTriggerEnter;

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnter.Invoke();
        }
    }
}