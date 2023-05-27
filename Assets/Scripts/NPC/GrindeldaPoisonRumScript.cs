using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindeldaPoisonRumScript : MonoBehaviour
{
    public CanvasRenderer poisonRumUI;
    public CanvasRenderer poisonUI;
    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        poisonRumUI.SetAlpha(0);

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    public void addPoisonToRum()
    {
        poisonRumUI.SetAlpha(100);
        poisonUI.SetAlpha(0);
        playerController.currentCheckpoint = 6;
    }
}
