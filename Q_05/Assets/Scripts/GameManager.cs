using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public float Score { get; set; }
    public bool isPaused;

    private void Awake()
    {
        SingletonInit();
        Score = 0.1f;
        isPaused = false;
    }

    public void Pause()
    {
        isPaused = true;
        //isPaused = !isPaused;
        //Time.timeScale = 0f;

    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
