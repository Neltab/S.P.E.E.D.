using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GUI gui;
    private Rigidbody _rb;
    private LevelManager _levelManager;
    public PlayerMovement playerMovement;
    public Vector3 rotation = new Vector3(0f,90f,0f);

    private void Start()
    {
        gui = FindObjectOfType<GUI>();
        _rb = GetComponent<Rigidbody>();
        _levelManager = FindObjectOfType<LevelManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Reset la position du joueur
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition(_levelManager.respawnPoint);
        }
        
        if (_levelManager.currentGeneration < _levelManager.slideGen)
        {
            playerMovement.isAllowed.crouch = false;
        }
        else
        {
            playerMovement.isAllowed.crouch = true;
        }
        if (_levelManager.currentGeneration < _levelManager.wallrunGen)
        {
            playerMovement.isAllowed.wallrun = false;
        }
        else
        {
            playerMovement.isAllowed.wallrun = true;
        }
        
    }

    public void ResetPosition(Vector3 pos)
    {
        gui.GetComponent<GUI>().ResetChrono();
        transform.position = pos;
        _rb.velocity = Vector3.zero;
        transform.parent.GetChild(1).transform.eulerAngles = rotation;
    }
}