using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackPickup : MonoBehaviour
{
    public Light backpackIndicatorLight;
    public CanvasRenderer backpackIndicatorCanvas;

    private void Start()
    {
        backpackIndicatorLight.intensity = 0;

        backpackIndicatorCanvas.SetAlpha(0);
    }

    public void OnMouseDown()
    {
        Destroy(gameObject);

        backpackIndicatorCanvas.SetAlpha(100);

        // Also need to trigger something to allow the player to pick up more items now
    }

    public void OnMouseEnter()
    {
        backpackIndicatorLight.intensity = 10;
    }

    public void OnMouseExit()
    {
        backpackIndicatorLight.intensity = 0;
    }
}
