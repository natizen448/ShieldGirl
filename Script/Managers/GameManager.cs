using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public enum GameState
    {
        Ready,
        InGame,
        Pause,
        Setting,
        End,
    }

public class GameManager : Singleton<GameManager>
{
   public GameState gameState = GameState.Ready;
    void Start()
    {

#if UNITY_IOS || UNITY_ANDROID
        Application.targetFrameRate = 60;
#else
        QualitySettings.vSyncCount = 1;
#endif
    }

}
