using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindeldaPoisonRumScript : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    public void addPoisonToRum()
    {
        playerController.SetPoisionRumGUI(100);
        playerController.SetPoisonGUI(0);
        playerController.currentCheckpoint = 6;
    }
}
