using UnityEngine;

public class PrisonCellDialogTriggers : MonoBehaviour
{

    public Light npcIndicatorLight;

    public void Start() {
        npcIndicatorLight.intensity = 0;
    }

    public void OnMouseEnter() {
        npcIndicatorLight.intensity = 5;
    }

    public void OnMouseExit() {
        npcIndicatorLight.intensity = 0;
    }

    private void OnMouseDown() {
        
            this.gameObject.GetComponents<DialogueTrigger>()[0].TriggerDialogue();

        
    }

}