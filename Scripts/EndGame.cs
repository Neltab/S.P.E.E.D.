using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Start()
    {
        text.text = GameManagerScript.instance.timeToClockFormat(GameManagerScript.globalTimer);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        GameManagerScript.instance.isInMenu = true;
        GameManagerScript.gameIsStarted = false;
        AudioManager.instance.Pause("GameMusic");
        AudioManager.instance.Play("MenuMusic");
        GameManagerScript.instance.ResetGlobalTimer();
        SceneManager.LoadScene("Menu");
    }
}
