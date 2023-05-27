using UnityEngine;

public class PrisonCellDialogTriggers : MonoBehaviour
{



    private void OnMouseDown() {
        
            this.gameObject.GetComponents<DialogueTrigger>()[0].TriggerDialogue();

        
    }

}