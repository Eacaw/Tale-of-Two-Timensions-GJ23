using UnityEngine;

public class WizardDialogScript : MonoBehaviour
{
    public Light npcIndicatorLight;
     private DialogueTrigger[] dialogItems;

    public void Start()
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

    public void OnMouseDown()
    {
        int currentCheckpoint = GameObject.FindGameObjectsWithTag("Player")[0]
            .GetComponent<PlayerController>()
            .currentCheckpoint;
        if (currentCheckpoint  == 0)
        {
            dialogItems[0].TriggerDialogue();
        }
        else if (currentCheckpoint == 1)
        {
            dialogItems[1].TriggerDialogue();
        }
        else if (currentCheckpoint == 2)
        {
            dialogItems[2].TriggerDialogue();
        }
        else if (currentCheckpoint == 4)
        {
            dialogItems[3].TriggerDialogue();
        }
        else if (currentCheckpoint == 6)
        {
            dialogItems[4].TriggerDialogue();
        }
        else if (currentCheckpoint == 9)
        {
            dialogItems[5].TriggerDialogue();
        }
        else if (currentCheckpoint == 10)
        {
            dialogItems[6].TriggerDialogue();
        }
        else if (currentCheckpoint == 11)
        {
            dialogItems[7].TriggerDialogue();
        }
    }

    public void OnMouseEnter()
    {
        npcIndicatorLight.intensity = 5;
    }

    public void OnMouseExit()
    {
        npcIndicatorLight.intensity = 0;
    }
}
