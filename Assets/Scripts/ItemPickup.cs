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

        if(gameObject.CompareTag("Key"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().hasKey = true;
        }

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
