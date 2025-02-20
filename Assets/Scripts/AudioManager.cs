using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioManager : SingletonProvider<AudioManager>
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Audio Clips")]
        public AudioClip bgmClip;
        public AudioClip gamePlayingClip;
        public AudioClip cubeDeathClip;
        public AudioClip screenTapClip;

        public void PlaySFX(AudioClip clip)
        {
            sfxSource.clip = clip;
            sfxSource.Play();
        }

        public void LoadAudioSettings()
        {
            sfxSource.mute = !PlayerController.I.GetSFXToggle();
        }

        public void MuteSFX()
        {
            sfxSource.mute = true;
            PlayerController.I.SetSFXToggle(false);
        }

        public void UnmuteSFX()
        {
            sfxSource.mute = false;
            PlayerController.I.SetSFXToggle(true);
        }
    }
}
