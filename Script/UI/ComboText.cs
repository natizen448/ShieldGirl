using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ComboText : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp_comboText;
    [SerializeField] private float m_alphaSpeed;

    public int currentCombo = 0;
    private float m_DestroyTime;

    Color alpha;

    private void Start()
    {
        tmp_comboText = GetComponent<TMP_Text>();
        alpha = tmp_comboText.color;
    }

    public void AddCombo()
    {
        m_DestroyTime = 1;
        alpha = ColorChange();
        currentCombo++;
        tmp_comboText.text = currentCombo.ToString() + " COMBO";
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        tmp_comboText.color = ColorChange();
    }

    public Color ColorChange()
    {
        if(currentCombo < 100)
        {
            return  Color.white;
        }
        else if(currentCombo >= 100 && currentCombo < 200) 
        {
            return new Color32(255, 125, 0, 237);
        }
        else
        { 
            return Color.red; 
        }

    }

    private void Update()
    {
        m_DestroyTime -= Time.deltaTime;

        if (m_DestroyTime < 0)
        {
            alpha.a = Mathf.Lerp(alpha.a, 0, m_alphaSpeed * Time.deltaTime);
           
        }
        tmp_comboText.color = alpha; 

        
    }
}
