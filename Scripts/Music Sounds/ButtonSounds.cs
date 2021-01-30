using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{
    private Button b;

    private void Start()
    {
        b = GetComponent<Button>();
        b.onClick.AddListener(Click);
    }

    void Click()
    {
        AudioManager.instance.Play("Tap");
    }
}
