using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class WaveSlider : MonoBehaviour
{
    [SerializeField] private Slider waveSlider;
    void Start()
    {
        waveSlider.maxValue = WaveController.Instance.m_currentWaveTime;
    }

    public void Init()
    {
        waveSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        if(GameManager.Instance.gameState != GameState.End)
        {
            waveSlider.value += Time.deltaTime;
        }
        
    }
}
