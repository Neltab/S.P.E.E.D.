using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EndPlatformController : MonoBehaviour
{
    private Rigidbody rb;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) // If we collide with the player, launch the next generation
        {
            levelManager.NextGeneration();
        }
    }
}
