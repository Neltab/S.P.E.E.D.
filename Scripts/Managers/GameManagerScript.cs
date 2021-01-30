using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public float mouseSensitivity;
    public static GameManagerScript instance;

    public GameObject optionsMenu;
    public static bool GameisPaused = false;
    public bool isInMenu = false;

    private bool finishedTuto = false;
    public bool gameHasEnded = false;
    public static string globalTimerText;

    public static float globalTimer = 0;
    public static bool gameIsStarted = false;
    private int minutes, secondes, milli;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        mouseSensitivity = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.3f); //rotate the skybox

        if (!isInMenu && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            if (!GameisPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 6 && !finishedTuto)
        {
            finishedTuto = true;
            AudioManager.instance.Pause("MenuMusic");
            AudioManager.instance.Play("GameMusic");
        }

        if (SceneManager.GetActiveScene().name == "Ending")
        {
            gameHasEnded = true;
        }

        if (gameIsStarted && !GameisPaused && !gameHasEnded)
        {
            globalTimer += Time.deltaTime;
        }
    }

    private void Start()
    {
        AudioManager.instance.Play("MenuMusic");
    }

    public void ResetGlobalTimer()
    {
        globalTimer = 0;
    }

    public string timeToClockFormat(float time)
    {
        int minutes = (int)Mathf.Floor(time / 60f);
        int secondes = (int)Mathf.Floor(time % 60f);
        int milli = (int)Mathf.Floor(time * 1000 % 1000);

        return $"{minutes.ToString().PadLeft(2, '0')}:{secondes.ToString().PadLeft(2, '0')}:{milli.ToString().PadLeft(3, '0')}";
    }
    
    public void PauseGame()
    {
        optionsMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        GameisPaused = true;
    }

    public void ResumeGame()
    {
        optionsMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
