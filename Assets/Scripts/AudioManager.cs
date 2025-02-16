using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

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

    public void PlayBGM()
    {
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public bool IsBGMOn()
    {
        return !bgmSource.mute;
    }

    public bool IsSFXOn()
    {
        return !sfxSource.mute;
    }

    public void LoadAudioSettings()
    {
        bgmSource.mute = PlayerPrefs.GetInt("Is_BGM_On", 1) == 0;
        sfxSource.mute = PlayerPrefs.GetInt("Is_SFX_On", 1) == 0;
    }

    public void ToggleBGM()
    {
        bgmSource.mute = !bgmSource.mute;
        PlayerPrefs.SetInt("Is_BGM_On", bgmSource.mute ? 0 : 1);
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        PlayerPrefs.SetInt("Is_SFX_On", sfxSource.mute ? 0 : 1);
    }
}
