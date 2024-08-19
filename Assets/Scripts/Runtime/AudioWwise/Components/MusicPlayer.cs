using UnityEngine;

namespace Z3.Audio.WwiseIntegration
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private SoundReference soundReference;

        private void Awake()
        {
            AudioManager.SetCurrentMusic(soundReference);
        }

        private void OnDisable()
        {
            AudioManager.RemoveMusic(soundReference);
        }
    }
}