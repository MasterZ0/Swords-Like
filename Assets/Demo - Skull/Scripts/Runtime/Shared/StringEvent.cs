using System;
using UnityEngine;

namespace Z3.DemoSkull.Shared
{
    public abstract class StringEvent : MonoBehaviour
    {
        public event Action<string> OnEventTrigger;

        protected void Invoke(string eventName) => OnEventTrigger?.Invoke(eventName);
    }
}