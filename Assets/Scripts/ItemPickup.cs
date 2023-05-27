using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Light hoverIndicatorLight;
    public CanvasRenderer canvasRenderer;
    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        hoverIndicatorLight.intensity = 0;

        // Hide the GUI icon for this item
        canvasRenderer.SetAlpha(0);

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    public void OnMouseDown()
    {
        if (gameObject.CompareTag("Backpack"))
        {
            playerController.hasBackpack = true;
        }

        if (!playerController.hasBackpack)
        {
            Debug.Log("You need a backpack to pick up items");
            return;
        }

        if (Vector3.Distance(transform.position, player.transform.position) > 6.0f)
        {
            Debug.Log("Too far away to pick up item");
            return;
        }

        // Pickup objects and update player controller
        if (gameObject.CompareTag("Key"))
        {
            playerController.hasKey = true;
        }

        if (gameObject.CompareTag("Amulet"))
        {
            playerController.hasAmulet = true;
            playerController.currentCheckpoint = 2;
        }

         if (gameObject.CompareTag("Poison"))
        {
            playerController.currentCheckpoint = 4;
        }

         if (gameObject.CompareTag("WizardJuice"))
        {
            playerController.currentCheckpoint = 10;
        }

        player.GetComponent<Animator>().SetTrigger("pickupItem");
        canvasRenderer.SetAlpha(100);
        Destroy(gameObject);
    }

    public void OnMouseEnter()
    {
        hoverIndicatorLight.intensity = 10;
    }

    public void OnMouseExit()
    {
        hoverIndicatorLight.intensity = 0;
    }
}
