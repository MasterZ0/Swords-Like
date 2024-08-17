using UnityEngine;
using System;

namespace Z3.DemoSkull
{
    public abstract class StringEvent : MonoBehaviour
    {
        public event Action<string> OnEventTrigger;

        protected void Invoke(string eventName) => OnEventTrigger?.Invoke(eventName);
    }
}