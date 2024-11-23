using UnityEngine;
using UnityEngine.Audio;

namespace Onion_AI
{
    public static class StaticAudioManager
    {
        public static void PlaySFX(AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }

        public static void PlayTheme(AudioSource audioSource, AudioClip audioClip, bool loop = true)
        {
            audioSource.loop = loop;
            audioSource.clip = audioClip;

            audioSource.Play();
        }

        public static void StopTheme(AudioSource audioSource)
        {
            if(audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}