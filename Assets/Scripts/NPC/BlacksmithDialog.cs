using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithDialog : MonoBehaviour
{
    private DialogueTrigger[] dialogItems;
    public Light npcIndicatorLight;

    void Start()
    {
        dialogItems = this.gameObject.GetComponents<DialogueTrigger>();

        npcIndicatorLight.intensity = 0;

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
        if (
            GameObject.FindGameObjectsWithTag("Player")[0]
                .GetComponent<PlayerController>()
                .currentCheckpoint < 6
        )
        {
            dialogItems[0].TriggerDialogue();
        }
        else
        {
            dialogItems[1].TriggerDialogue();
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
