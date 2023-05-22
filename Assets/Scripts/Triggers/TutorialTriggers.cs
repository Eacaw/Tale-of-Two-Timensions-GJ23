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
            // Find any other game objects with the tag "InstructionUI" and set them to inactive
            GameObject[] instructionUIs = GameObject.FindGameObjectsWithTag("InstructionalUI");
            foreach (GameObject instructionUI in instructionUIs)
            {
                instructionUI.SetActive(false);
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