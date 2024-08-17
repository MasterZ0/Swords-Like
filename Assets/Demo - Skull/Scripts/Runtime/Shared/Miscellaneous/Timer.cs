using System;
using UnityEngine;

namespace Z3.DemoSkull.Shared
{
    public class Timer
    {
        public event Action OnCompleted;

        public float Progression => Counter / timeForEvent;
        public bool IsCompleted => Counter >= timeForEvent;
        public float Counter { get; private set; }

        private float timeForEvent;

        public void Reset(float timeToCallEvent = 0f)
        {
            timeForEvent = timeToCallEvent;
            Counter = 0f;
        }

        public void FixedTick()
        {
            bool wasCompleted = IsCompleted;

            Counter += Time.fixedDeltaTime;

            if (!wasCompleted && IsCompleted)
                OnCompleted?.Invoke();
        }
    }
}