using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Light hoverIndicatorLight;
    public CanvasRenderer canvasRenderer;

    private void Start()
    {
        hoverIndicatorLight.intensity = 0;

        canvasRenderer.SetAlpha(0);
    }

    public void OnMouseDown()
    {
        Destroy(gameObject);

        canvasRenderer.SetAlpha(100);

        // If it's the backpack, update the player's inventory
        if (gameObject.CompareTag("Backpack"))
        {
            GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>()
            .hasBackpack = true;
        }

        // Only allow picking up other items if the player has a backpack
        if(!GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>()
            .hasBackpack)
        {
            return;
        }
        // Functional Game Objects
        if (gameObject.CompareTag("Key"))
        {
            GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>()
            .hasKey = true;
        }

        if (gameObject.CompareTag("Amulet"))
        {
            GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>()
            .hasAmulet = true;
        }

        GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<Animator>()
            .SetTrigger("pickupItem");
        // Also add the item to the player's inventory
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
