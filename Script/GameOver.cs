using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : Singleton<GameOver>
{
    [SerializeField] private GameObject obj_gameOverPanel;
    [SerializeField] private TMP_Text tmp_score;
    [SerializeField] private TMP_Text tmp_gold;

    [SerializeField] private GameObject obj_Player;

    PlayerAttack playerAttack;
    PlayerMovement playerMovement;
    PlayerBlock playerBlock;
    public WaveSlider waveSlider;

    private void Start()
    {
        playerAttack = obj_Player.GetComponent<PlayerAttack>();
        playerMovement = obj_Player.GetComponent<PlayerMovement>();
        playerBlock = obj_Player.GetComponent<PlayerBlock>();
    }

    public void GameOverSet()
    {
        GameManager.Instance.gameState = GameState.End;
        obj_gameOverPanel.SetActive(true);
        tmp_score.text = ScoreBoard.Instance.m_score.ToString();
        tmp_gold.text = ScoreBoard.Instance.m_gold.ToString();


    }
    public void Restart()
    {
        ScoreBoard.Instance.Init();
        WaveController.Instance.Init();
        ObjectManager.Instance.Init();
        playerAttack.InIt();
        playerMovement.InIt();
        playerBlock.Init();
        waveSlider.Init();
        obj_gameOverPanel.SetActive(false);
        GameManager.Instance.gameState = GameState.InGame;
    }
}
