using UnityEngine;
using Z3.Utils;

namespace Z3.Audio.WwiseIntegration
{
    /// <summary>
    /// SoundReference Scriptable Object. You can give position through Transform, Vector2 or nothing at all.
    /// Store a SoundInstance through PlaySound to have more control of it, if needed.
    /// </summary>
    [CreateAssetMenu(menuName = Z3Path.ScriptableObjects + "Sound Data", fileName = "NewSoundData")]
    public class SoundData : ScriptableObject
    {
        public AK.Wwise.Event eventReference;

        public SoundInstance PlaySound(Transform transform = null)
        {
            return AudioManager.PlaySound(eventReference, transform);
        }

        //public SoundInstance PlaySound(Vector3 position)
        //{
        //    return AudioManager.PlaySound(eventReference, position);
        //}
    }
}