

namespace Z3.Audio.WwiseIntegration
{
    /// <summary>
    /// An instance of the sound. Use SoundReference or SoundData's "PlaySound" method to receive a SoundInstance and keep track of the audio.
    /// This class has all the values related to the currently playing sound.
    /// </summary>
    public class SoundInstance
    {
        private uint eventInstance;


        public SoundInstance(uint eventInstance)
        {
            this.eventInstance = eventInstance;
        }

        public void Start()
        {

        }

        /// <summary>
        /// The fade will only work if the sound has a fade inside FMOD.
        /// </summary>
        //public void StopWithFade() => eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
        //public void StopImmediate() => eventInstance.stop(STOP_MODE.IMMEDIATE);
        //public void Pause() => eventInstance.setPaused(true);
        //public void Unpause() => eventInstance.setPaused(false);
        //public bool SoundFinished()
        //{
        //    return false;
        //}

       // public void SetParameterByName(string name, float value, bool ignoreSeekSpeed = false) => eventInstance.setParameterByName(name, value, ignoreSeekSpeed);

        public static implicit operator bool(SoundInstance instance)
        {
            return instance != null;
        }
    }
}