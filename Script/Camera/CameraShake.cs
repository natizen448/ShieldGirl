using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float m_shakeAmount;
    [SerializeField] private float m_shakeTime;
    [SerializeField] private GameObject obj_redPanel;

    public void VibrateForTime(float power ,float time,bool isOnDamage)
    {
        m_shakeAmount = power;
        m_shakeTime = time;

        if(isOnDamage)
        {
            obj_redPanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (m_shakeTime > 0)
        {   

            transform.position = Random.insideUnitSphere * m_shakeAmount + transform.position;
            m_shakeTime -= Time.deltaTime;
        }

        else
        {
            m_shakeTime = 0.0f;
            obj_redPanel.SetActive(false);
        }


    }
}
