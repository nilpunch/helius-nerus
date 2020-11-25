﻿using UnityEngine;

public class GamePauserOnPause : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Pause.PauseGame();
        }
        else
        {
            if (TransitionScene.Instance.GetActiveScene() != (int)Scenes.INGAME)
                Pause.UnpauseGame();
        }
    }
}
