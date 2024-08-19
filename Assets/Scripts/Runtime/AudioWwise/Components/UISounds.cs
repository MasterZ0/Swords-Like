using UnityEngine;
using UnityEngine.EventSystems;

namespace Z3.Audio.WwiseIntegration
{
    public class UISounds : MonoBehaviour, ISelectHandler, ISubmitHandler, ICancelHandler, IPointerClickHandler
    {
        [SerializeField] private bool ignoreSubmit;
        [SerializeField] private bool ignoreCancel;
        [SerializeField] private bool ignoreSelect;

        private static bool SkipSelect = true;

        public static void IgnoreNext() => SkipSelect = true;

        public void OnSubmit(BaseEventData eventData)
        {
            if (ignoreSubmit)
                return;

            SkipSelect = true;

            OnPlaySubmit();
        }
        public void OnCancel(BaseEventData eventData)
        {
            if (ignoreCancel)
                return;

            SkipSelect = true;

            OnPlayCancel();
        }

        public void OnSelect(BaseEventData eventData)
        {
            bool skip = SkipSelect;
            SkipSelect = false;

            if (ignoreSelect || skip)
                return;

            OnPlaySelect();
        }

        public void OnPlaySubmit()
        {
            AudioManager.PlayUISound(UISound.Submit);
        }
        public void OnPlayCancel()
        {
            AudioManager.PlayUISound(UISound.Cancel);
        }

        public void OnPlaySelect()
        {
            AudioManager.PlayUISound(UISound.Select);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnSubmit(eventData);
            }
        }
    }
}