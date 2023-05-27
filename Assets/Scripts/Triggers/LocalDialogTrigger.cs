using UnityEngine;

public class LocalDialogTrigger : MonoBehaviour
{

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !hasTriggered) {
            this.gameObject.GetComponents<DialogueTrigger>()[0].TriggerDialogue();
            hasTriggered = true;
        }
    }

}