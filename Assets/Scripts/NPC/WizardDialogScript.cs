using UnityEngine;

public class WizardDialogScript : MonoBehaviour
{
    public Light npcIndicatorLight;

    public void OnMouseDown()
    {
        this.gameObject.GetComponents<DialogueTrigger>()[0].TriggerDialogue();
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