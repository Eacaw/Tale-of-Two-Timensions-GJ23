using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    public GameObject instructionUI;

    private bool hasUIBeenShownBefore;

    void Start()
    {
        instructionUI.SetActive(false);
        hasUIBeenShownBefore = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Canvas otherCanvas = FindFirstObjectByType<Canvas>();
            if(otherCanvas)
            {
                // This seems to work but idfk where it goes cuase it's
                // still shown as dispalyed in hierarchy shrugz
                otherCanvas.enabled = false;
            }

            // Ensure trigger isn't hit more than once
            if (hasUIBeenShownBefore == false)
            {
                instructionUI.SetActive(true);
                hasUIBeenShownBefore = true;
            }
        }
    }

    public void CloseInstructionPanel()
    {
        instructionUI.SetActive(false);
    }

}