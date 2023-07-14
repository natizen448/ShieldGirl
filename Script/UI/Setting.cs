using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{

    [Space(10f)]
    [Header("오디오")]   
    [SerializeField] private AudioMixer m_audioMixer;
    [SerializeField] private Slider m_bgmSlider;
    [SerializeField] private Slider m_sfxSlider;

    [Space(10f)]
    [Header("버튼")]
    [SerializeField] private GameObject obj_settingPanel;

    GameState saveState;

    private void Awake()
    {
        m_bgmSlider.value = SoundManager.Instance.m_bgmSoundValue;
        m_sfxSlider.value = SoundManager.Instance.m_sfxSoundValue;
        m_audioMixer.SetFloat("BGM", Mathf.Log10(m_bgmSlider.value) * 20);
        m_audioMixer.SetFloat("SFX", Mathf.Log10(m_sfxSlider.value) * 20);
    }

    public void SetBGMVolume()
    {
        m_audioMixer.SetFloat("BGM",Mathf.Log10(m_bgmSlider.value) * 20);
        SoundManager.Instance.m_bgmSoundValue = m_bgmSlider.value;
    }
    public void SetSFXVolume()
    {
        m_audioMixer.SetFloat("SFX", Mathf.Log10(m_sfxSlider.value) * 20);
        SoundManager.Instance.m_sfxSoundValue = m_sfxSlider.value;
    }

    public void Btn_OpenSetting()
    {
        saveState = GameManager.Instance.gameState;
        GameManager.Instance.gameState = GameState.Setting;
        obj_settingPanel.SetActive(true);
    }

    public void Btn_CloseSetting()
    {
        GameManager.Instance.gameState = saveState;
        obj_settingPanel.SetActive(false);
    }
}
