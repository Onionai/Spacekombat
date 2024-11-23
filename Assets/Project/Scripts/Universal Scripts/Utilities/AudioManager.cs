using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    [System.Serializable]
    public class Sound
    {
        public int id;
        public string soundName;

        [Header("Audio Info")]
        public AudioClip audioClip;
        public AudioSource audioSource;

        [Header("Audio Parameters")]
        public bool canLoopSound;
        [Range(0f,1f)] public float pitch;
        [Range(0f,1f)] public float volume;

        public void InitializeSounds()
        {
            audioSource.pitch = pitch;
            audioSource.volume = volume;
        }

        public void PlaySound()
        {
            if(canLoopSound)
            {
                StaticAudioManager.PlayTheme(audioSource, audioClip);
                return;
            }
            StaticAudioManager.PlaySFX(audioSource, audioClip);
        }
    }

    public class AudioManager : MonoBehaviour
    {
        
        [Header("Audio Clips")]
        public List<Sound> soundList = new();

        private void Awake()
        {
            foreach(var sound in soundList)
            {
                sound.InitializeSounds();
            }
        }

        public void StopSound(int soundID)
        {
            Sound sound = soundList.Find(x => x.id == soundID);

            if(sound != null)
            {
                StaticAudioManager.StopTheme(sound.audioSource);
            }
        }

        public void PlaySound(int soundID)
        {
            Sound sound = soundList.Find(x => x.id == soundID);

            if(sound != null)
            {
                sound.PlaySound();
            }
        }
    }
}
