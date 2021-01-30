using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private LevelManager _levelManager;
    public List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>(); 

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < textList.Count; i++)
        {
            if (i == _levelManager.currentGeneration && _levelManager.waitForSpace)
            {
                textList[i].enabled = true;
            }
            else
            {
                textList[i].enabled = false;
            }
        }
    }
}
