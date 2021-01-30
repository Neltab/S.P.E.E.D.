using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{

    public AudioMixer masterMixer;

    public float mouseSensitivity;
    
    void Start()
    {

        List<string> options = new List<string>(); //create list
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(0.5f) * 20);
        
        SetMouseSens(300f);

    }

    //volume sliders
    public void SetMasVolume (float volume)
    {
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    //---

 
    public void SetMouseSens(float num)
    {
        GameManagerScript.instance.mouseSensitivity = num;
    }
}