using UnityEngine;
using System.Collections.Generic;
using Z3.Utils;
using Event = AK.Wwise.Event;

namespace Z3.Audio.WwiseIntegration
{
    public enum SoundGroup
    {
        Master,
        Music,
        SFX,
        Voice
    }

    public enum UISound
    {
        Submit,
        Cancel,
        Select
    }

    /// <summary>
    /// Manage the sound requests and returns a SoundInstance.
    /// </summary>
    public class AudioManager : Monostate<AudioManager>
    {
        [Header("UI")]
        [SerializeField] private SoundReference submit;
        [SerializeField] private SoundReference cancel;
        [SerializeField] private SoundReference select;

        private static SoundInstance currentMusic;

        private static readonly List<SoundInstance> pauseSoundsList = new List<SoundInstance>();
        private static readonly List<SoundInstance> stopSoundsList = new List<SoundInstance>();

        #region Public Methodsx
        public static void RemoveMusic(SoundReference music)
        {/*
            if (currentMusic != music)
                return;

            currentMusic.StopWithFade();*/
        }

        public static void SetCurrentMusic(SoundReference music)
        {/*
            if (currentMusic == music)
                return;

            if (currentMusic)
            {
                currentMusic.StopWithFade();
                currentMusic = null;
            }

            if (music)
                currentMusic = music.PlaySound();*/
        }

        public static void PlayUISound(UISound soundType)
        {/*
            SoundReference sound = soundType switch
            {
                UISound.Submit => Instance.submit,
                UISound.Cancel => Instance.cancel,
                UISound.Select => Instance.select,
                _ => throw new NotImplementedException(),
            };
            
            sound.PlaySound();*/
        }

        /// <summary> Set the VCA mixer </summary>
        /// <param name="volume">0 to 1</param>
        public static void SetVolume(SoundGroup soundGroup, float volume)
        {/*
            VCA vca = RuntimeManager.GetVCA($"vca:/{soundGroup}");
            vca.setVolume(volume);*/
        }
        /*
        public static void AddToAutoStopList(SoundInstance instance) => stopSoundsList.Add(instance);

        public static void StopSounds()
        {
            foreach (SoundInstance instance in stopSoundsList)
            {
                instance.StopWithFade();
            }

            stopSoundsList.Clear();
        }*/

        public static void AddToPauseSoundsList(SoundInstance instance) => pauseSoundsList.Add(instance);
        /*
        public static void PauseSounds()
        {
            foreach (SoundInstance instance in pauseSoundsList.ToList())
            {
                if (instance.SoundFinished())
                {
                    pauseSoundsList.Remove(instance);
                }
                else
                {
                    instance.Pause();
                }
            }
        }

        public static void UnpauseSounds()
        {
            foreach (SoundInstance instance in pauseSoundsList)
            {
                instance.Unpause();
            }
        }*/
        #endregion
        
        internal static SoundInstance PlaySound(Event eventReference, Transform transform)
        {
            eventReference.Post(transform.gameObject);

            uint instanceId = AkSoundEngine.PostEvent(eventReference.Id, transform.gameObject);

            SoundInstance soundInstance = new SoundInstance(instanceId);
            soundInstance.Start();
            return soundInstance;
        }

        //internal static SoundInstance PlaySound(Event eventReference, Vector3 position)
        //{
        //    uint instanceId = AkSoundEngine.PostEvent(eventReference.Id, position);
        //    EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        //    eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(position));
        //    SoundInstance soundInstance = new SoundInstance(eventInstance);
        //    soundInstance.Start();
        //    return soundInstance;
        //}
    }
}