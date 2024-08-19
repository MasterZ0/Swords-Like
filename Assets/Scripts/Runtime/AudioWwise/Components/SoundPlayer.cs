using UnityEngine;

namespace Z3.Audio.WwiseIntegration
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundReference soundReference;

        private SoundInstance instance;

        private void OnEnable()
        {
            //instance = soundReference.PlaySound(transform);
            //AudioManager.AddToPauseSoundsList(instance);
        }

        private void OnDisable()
        {
          // instance.StopWithFade();
        }
    }
}