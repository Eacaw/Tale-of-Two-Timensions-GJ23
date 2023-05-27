using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindeldaDialog : MonoBehaviour
{
    private DialogueTrigger[] dialogItems;
    public Light npcIndicatorLight;

    void Start()
    {
        dialogItems = this.gameObject.GetComponents<DialogueTrigger>();

        npcIndicatorLight.intensity = 0;

        // sort the dialogItems based on their DialogId
        for (int i = 0; i < dialogItems.Length; i++)
        {
            for (int j = i + 1; j < dialogItems.Length; j++)
            {
                if (dialogItems[i].DialogID > dialogItems[j].DialogID)
                {
                    DialogueTrigger temp = dialogItems[i];
                    dialogItems[i] = dialogItems[j];
                    dialogItems[j] = temp;
                }
            }
        }
    }

    void OnMouseDown()
    {
        int currentCheckpoint = GameObject.FindGameObjectsWithTag("Player")[0]
            .GetComponent<PlayerController>()
            .currentCheckpoint;
        if (currentCheckpoint < 5)
        {
            dialogItems[0].TriggerDialogue();
        }
        else if (currentCheckpoint == 5)
        {
            dialogItems[1].TriggerDialogue();
        }
        else
        {
            dialogItems[2].TriggerDialogue();
        }
    }

    void OnMouseEnter()
    {
        npcIndicatorLight.intensity = 5;
    }

    void OnMouseExit()
    {
        npcIndicatorLight.intensity = 0;
    }
}
