using UnityEngine;

public class WizardDialogScript : MonoBehaviour
{
    public Light npcIndicatorLight;
    private DialogueTrigger[] dialogItems;

    public GameObject AmuletCook;

    public void Start()
    {
        dialogItems = this.gameObject.GetComponents<DialogueTrigger>();

        npcIndicatorLight.intensity = 0;
        AmuletCook.SetActive(false);

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
        PlayerController playerController = GameObject.FindGameObjectsWithTag("Player")[
            0
        ].GetComponent<PlayerController>();
        int currentCheckpoint = playerController.currentCheckpoint;

        // When in doubt, if/else it out
        // dont judge me
        if (currentCheckpoint == 0)
        {
            dialogItems[0].TriggerDialogue();
            playerController.currentCheckpoint = 1;
        }
        else if (currentCheckpoint == 1)
        {
            dialogItems[1].TriggerDialogue();
        }
        else if (currentCheckpoint == 2 || currentCheckpoint == 3)
        {
            dialogItems[2].TriggerDialogue();
            playerController.currentCheckpoint = 3;
        }
        else if (currentCheckpoint == 4 || currentCheckpoint == 5)
        {
            playerController.currentCheckpoint = 5;
            dialogItems[3].TriggerDialogue();
        }
        else if (currentCheckpoint == 6 || currentCheckpoint == 7 || currentCheckpoint == 8)
        {
            if (currentCheckpoint == 6)
            {
                playerController.currentCheckpoint = 7;
            }

            dialogItems[4].TriggerDialogue();
        }
        else if (currentCheckpoint == 9)
        {
            dialogItems[5].TriggerDialogue();
        }
        else if (currentCheckpoint == 10)
        {
            dialogItems[6].TriggerDialogue();
            AmuletCook.SetActive(true);
        }
        else if (currentCheckpoint == 11)
        {
            dialogItems[7].TriggerDialogue();
            playerController.currentCheckpoint = 12;
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
