using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformController : MonoBehaviour
{
    public int generation = 0;
    private LevelManager levelManager;
    public Material whiteMat, colorMat;
    public bool isColored = false;
    
    
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    
    void Update()
    {
        if (isColored && generation == levelManager.currentGeneration)
        {
            gameObject.GetComponent<MeshRenderer>().material = colorMat;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = whiteMat;
        }
    }
}
