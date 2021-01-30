using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //Level Transitions
    [Header("Transitions")]
    public Animator levelTransition;
    public float levelTransitionTime = 1f;
    public Animator midLevelTransition;
    public float midLevelTransitionTime = 0.5f;
    private bool isInLevelCameraMode = true; // If the player is watching through the camera level
    public bool waitForSpace = true;
    [Space(10)]
        
    // Times and generations
    [Header("Generations")]
    [SerializeField]
    public List<int> timeList = new List<int>();
    public int currentGeneration = 0;
    private int playerGeneration = -1; // to avoid the jump of generations while touching the end platform
    public Vector3 respawnPoint;

    //Skill unlock generations
    [Header("Skill unlocks")]
    public int jumpGen = 0;
    public int slideGen = 0;
    public int wallrunGen= 0;
    
    [Header("Other elements")]
    public GUI gui;
    private PlayerManager pm;
    public Camera playerCam;
    public Camera levelCam; //Added in the editor
    private PlateformController[] platforms;
    public GameObject endChoice;
    public TextMeshProUGUI buttonNextText;
    public Canvas pressSpaceCanvas;

    private void Awake()
    {
        // We must set this up before the start method because the findobject method takes time to execute
        gui = FindObjectOfType<GUI>();
        pm = FindObjectOfType<PlayerManager>();
    }

    void Start()
    {
        platforms = FindObjectsOfType<PlateformController>(); //At the beginning, we desactivate all platforms not to be showed in the active generation
        foreach (var plat in platforms)
        {
            if (plat.generation != 0)
            {
                plat.gameObject.SetActive(false);
            }
        }
        Invoke("LateStart", 0.1f);
    }

    private void LateStart() // All theses lines need to be executed after the pm and gui variables are set, we need to delay this function by a bit
    {
        gui.UpdateTimerDisplay(timeList[currentGeneration]);
        
        playerCam = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>(); // Find the player camera and references it
        
        //Setup the scene for new level
        levelCam.enabled = true; //enable the level camera
        playerCam.enabled = false;
        pm.playerMovement.isAllowed.move = false; //We disable the movement for the player
        
        // Desactivate the current timer
        gui.chronoText.enabled = false;
    }

    private void Update()
    {
        if (isInLevelCameraMode && Input.GetKeyDown(KeyCode.Space))
        {
            if (waitForSpace)
            {
                AudioManager.instance.Play("Tap");
                PlayNextGeneration();
            }
            else
            {
                waitForSpace = false;
                UpdateNextGeneration();
            }
        }
        
        
        if (isInLevelCameraMode && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(CReplayCurrentGeneration()); // We replay the previous generation
        }

        if (isInLevelCameraMode && waitForSpace)
        {
            pressSpaceCanvas.enabled = true;
        }
        else
        {
            pressSpaceCanvas.enabled = false;
        }
        
    }
    
    IEnumerator CSwitchCamerasOut()
    {
        midLevelTransition.SetTrigger("Start");
        
        //We are now in level camera view
        isInLevelCameraMode = true;
        
        yield return new WaitForSeconds(midLevelTransitionTime);
        
        //We switch cameras
        levelCam.enabled = true;
        playerCam.enabled = false;
        
        //We hide the gui and the player cannot move
        gui.chronoText.enabled = false;
        gui.pressR.enabled = false;
        if (currentGeneration + 1 >= timeList.Count)
        {
            buttonNextText.text = "Next Level";
        }
        else
        {
            buttonNextText.text = "Next Generation";
        }

        pm.playerMovement.isAllowed.move = false;
        pm.playerMovement.isAllowed.jump = false;
        
        
        pm.ResetPosition(respawnPoint); // we tp the player back to the beginning
        
        //Activate the option buttons
        endChoice.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        waitForSpace = false; // avoid duplicate inputs ?
    }
    public IEnumerator CNextGeneration()
    {
        isInLevelCameraMode = false;
        
        waitForSpace = false;
        
        midLevelTransition.SetTrigger("Start");
        
        yield return new WaitForSeconds(midLevelTransitionTime);
        
        //Desactivate the buttons
        endChoice.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //We switch cameras
        playerCam.enabled = true;
        levelCam.enabled = false;
        gui.pressR.enabled = true;
        
        yield return new WaitForSeconds(1.5f * midLevelTransitionTime); //after some time we reactivate the chrono and the player can move
        gui.chronoText.enabled = true;
        gui.ResetChrono();
        pm.playerMovement.isAllowed.move = true;
        pm.playerMovement.isAllowed.jump = true;

        playerGeneration++;
    }
    

    public void NextGeneration()
    {
        //Add test to verify if the timer is correct before incrementing
        if (gui.currentTime <= timeList[currentGeneration] && playerGeneration == currentGeneration) //calculs only with seconds / -1 because of millisecs
        {
            if (!isInLevelCameraMode)
            {
                //Génération validée
                StartCoroutine(CSwitchCamerasOut()); // This is an out transition, to the level camera
            }
        }
    }

    public void UpdateNextGeneration()
    {
        currentGeneration++;

        if (currentGeneration >= timeList.Count) // if we finished the level
        {
            endChoice.SetActive(false);
            LoadNextLevel();
            return;
        }
        
        //Each time the generation is updated, we activate the platforms 
        foreach (var plat in platforms)
        {
            if (currentGeneration >= plat.generation)
            {
                plat.gameObject.SetActive(true);
            }
        }
        
        //Update the chrono objective display
        gui.UpdateTimerDisplay(timeList[currentGeneration]);
        
        waitForSpace = true;
        
        //Desactivate the buttons
        endChoice.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void PlayNextGeneration()
    {
        StartCoroutine(CNextGeneration());
    }

    public void ReplayCurrentGeneration()
    {
        StartCoroutine(CReplayCurrentGeneration());
    }
    
    IEnumerator CReplayCurrentGeneration()
    {
        midLevelTransition.SetTrigger("Start");
        
        yield return new WaitForSeconds(midLevelTransitionTime);
        
        //Desactivate the buttons
        endChoice.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        //We switch cameras
        playerCam.enabled = true;
        levelCam.enabled = false;
        gui.pressR.text = "Press R to reset";
        
        yield return new WaitForSeconds(1.5f * midLevelTransitionTime); //after some time we reactivate the chrono and the player can move
        gui.chronoText.enabled = true;
        gui.ResetChrono();
        pm.playerMovement.isAllowed.move = true;
        pm.playerMovement.isAllowed.jump = true;
        
        isInLevelCameraMode = false;
    }
    
    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        levelTransition.SetTrigger("Start");
        
        yield return new WaitForSeconds(levelTransitionTime);
        AudioManager.instance.Play("Whoosh");

        SceneManager.LoadScene(levelIndex);
    }
}
