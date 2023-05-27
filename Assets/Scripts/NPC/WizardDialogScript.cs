using UnityEngine;

public class WizardDialogScript : MonoBehaviour
{
    public Light npcIndicatorLight;
    public int currentDialogIndex = 0;

    public void Start()
    {
        npcIndicatorLight.intensity = 0;
    }

    public void OnMouseDown()
    {
        DialogueTrigger[] dialogItems = this.gameObject.GetComponents<DialogueTrigger>();

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

        dialogItems[currentDialogIndex].TriggerDialogue();

        if (currentDialogIndex == 0)
        {
            currentDialogIndex = 1;
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
