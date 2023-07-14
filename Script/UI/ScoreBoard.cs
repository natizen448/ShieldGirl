using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : Singleton<ScoreBoard>
{
    [Header("Score")]
    [SerializeField] private TMP_Text tmp_scoreBoard;
    public int m_score;

    [Header("HP")]
    [SerializeField] private GameObject[] obj_hpImage;
    [HideInInspector] public int m_hp = 3;
    private int m_defaultHP = 3;

    [Header("Gold")]
    [SerializeField] private TMP_Text tmp_goldBoard;
    public int m_gold = 0;

    [Header("Combo")]
    [SerializeField] private ComboText comboText;

    [SerializeField] private CameraShake cameraShake;


    public void AddScore(int _score)
    {
        comboText.AddCombo();

        m_score += _score;
        tmp_scoreBoard.text = m_score.ToString(); 
    }

    public void OnDamaged(int _damage)
    {
        obj_hpImage[m_hp - 1].GetComponent<Image>().enabled = false;

        m_hp -= _damage;

        cameraShake.VibrateForTime(0.07f,0.1f,true);

        CheckGameOver();

        comboText.ResetCombo();
    }
    
    public void AddHP()
    {
        m_hp += 1;

        obj_hpImage[m_hp - 1].GetComponent<Image>().enabled = true;
    }


    private void CheckGameOver()
    {
        if (m_hp == 0)
        {
            GameOver.Instance.GameOverSet();
        }
    }

    public void Init()
    {
        m_hp = m_defaultHP;
        m_score = 0;
        tmp_scoreBoard.text = m_score.ToString();
        comboText.ResetCombo();
        for (int i = 0; i < obj_hpImage.Length; i++)
        {
            obj_hpImage[i].GetComponent <Image>().enabled = true;
        }
        obj_hpImage[3].GetComponent<Image>().enabled = false;

    }

    public void AddGold(int _gold)
    {
        m_gold += _gold;
        tmp_goldBoard.text = m_gold.ToString();
    }


}
