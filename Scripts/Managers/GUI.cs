using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public TextMeshProUGUI chronoText;
    public TextMeshProUGUI objectiveChronoText;
    public TextMeshProUGUI pressR;
    public TextMeshProUGUI chronoGlobal;
    private float startTime;
    public float currentTime;

    public LevelManager levelManager;
    
    
    void Start()
    {
        ResetChrono();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time - startTime;
        chronoText.text = timeToClockFormat(currentTime);
        chronoGlobal.text = timeToClockFormat(GameManagerScript.globalTimer);
    }

    public void ResetChrono()
    {
        startTime = Time.time;
    }

    public void UpdateTimerDisplay(int seconds)
    {
        objectiveChronoText.text = "Target : " + seconds + " s.";
    }

    private string timeToClockFormat(float time)
    {
        int minutes = (int)Mathf.Floor(time / 60f);
        int secondes = (int)Mathf.Floor(time % 60f);
        int milli = (int)Mathf.Floor(time * 1000 % 1000);

        return $"{minutes.ToString().PadLeft(2, '0')}:{secondes.ToString().PadLeft(2, '0')}:{milli.ToString().PadLeft(3, '0')}";
    }


}
