using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Canvas optionsMenuCanvas;
    public Canvas mainMenuCanvas;
    void Start()
    {
        GameManagerScript.instance.gameHasEnded = false;
        optionsMenuCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }

    public void StartGame()
    {
        GameManagerScript.instance.isInMenu = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManagerScript.gameIsStarted = true;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnableOptionsMenu()
    {
        mainMenuCanvas.enabled = false;
        optionsMenuCanvas.enabled = true;
    }

    public void EnableMainMenu()
    {
        optionsMenuCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }
}
