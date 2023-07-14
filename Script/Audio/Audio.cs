using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource m_SFXAudioSource;
    [SerializeField] private AudioSource m_BGMAudioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


}
