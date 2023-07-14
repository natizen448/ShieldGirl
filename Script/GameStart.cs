using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{   



    public void Btn_GameStart()
    {
        GameManager.Instance.gameState = GameState.InGame;
        SceneManager.LoadScene(1);
    }
}
