using System.Linq;
using System;
using UnityEngine.Events;
using UnityEngine;

namespace Z3.GMTK2024.Shared
{
    public class AnimationEventTrigger : MonoBehaviour
    {
        [Serializable]
        public class EventReference
        {
            public string eventName;
            public int layer;

            [Range(0f, 1f)]
            public float minWeight = 0.5f;
            public UnityEvent unityEvent;
            
            public float LastTrigger { get; set; }
        }

        [Header("Event Trigger")]
        [SerializeField] private Animator animator;
        [SerializeField] private EventReference[] eventReferences;

        public Action<string> OnEventTrigger { get; internal set; }

        public void OnEvent(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight < 0.5f)
                return;

             EventReference reference = eventReferences.First(e => e.eventName == animationEvent.stringParameter);

            if (animationEvent.animatorClipInfo.weight < reference.minWeight /*&& animator.IsInTransition(reference.layer)*/)
                return;

            reference.unityEvent.Invoke();
            OnEventTrigger?.Invoke(reference.eventName);
        }
    }
}