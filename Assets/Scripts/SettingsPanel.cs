using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Patterns.SingletonPattern;
using UnityEngine;

public class SettingsPanel : SingletonProvider<SettingsPanel>
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private TMPro.TMP_Text _bgmText;
    [SerializeField] private TMPro.TMP_Text _sfxText;

    public void Show()
    {
        _panel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _panel.gameObject.SetActive(false);
    }

    public void ToggleBGM()
    {
        AudioManager.Instance.ToggleBGM();
        _bgmText.text = "MUSIC (" + (AudioManager.Instance.IsBGMOn() ? "ON" : "OFF") + ")";
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
        _sfxText.text = "SOUND (" + (AudioManager.Instance.IsSFXOn() ? "ON" : "OFF") + ")";
    }
}
