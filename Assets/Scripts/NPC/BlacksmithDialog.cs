using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithDialog : MonoBehaviour
{
    private DialogueTrigger[] dialogItems;
    public Light npcIndicatorLight;
    [SerializeField] private FMODUnity.EventReference CoughEventPath;

    private bool hasDiedAlready = false;

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
        PlayerController playerController = GameObject.FindGameObjectsWithTag("Player")[
            0
        ].GetComponent<PlayerController>();

        if(playerController.hasKilledBlacksmith)
        {
            dialogItems[2].TriggerDialogue();
        }

        if (playerController.currentCheckpoint < 6)
        {
            dialogItems[0].TriggerDialogue();
        }
        else if (playerController.currentCheckpoint < 8 && !this.hasDiedAlready)
        {
            dialogItems[1].TriggerDialogue();
            GameObject.FindGameObjectsWithTag("Blacksmith")[0]
                .GetComponent<Animator>()
                .SetTrigger("isDying");
            playerController.currentCheckpoint = 8;
            playerController.hasKilledBlacksmith = true;
            this.hasDiedAlready = true;

            FMOD.Studio.EventInstance cough = FMODUnity.RuntimeManager.CreateInstance(CoughEventPath);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(cough, transform, GetComponent<Rigidbody>());

            cough.start();
            cough.release();
        }
        
        
        playerController.SetPoisionRumGUI(false);
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
