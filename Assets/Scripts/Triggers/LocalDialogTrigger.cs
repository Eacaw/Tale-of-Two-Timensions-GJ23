using UnityEngine;

public class LocalDialogTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        // get player instance

        PlayerController playerController = GameObject.FindGameObjectsWithTag("Player")[
            0
        ].GetComponent<PlayerController>();

        if (other.CompareTag("Player") && !playerController.hasTriggeredStartTrigger) {
            this.gameObject.GetComponents<DialogueTrigger>()[0].TriggerDialogue();
            playerController.hasTriggeredStartTrigger = true;
        }
    }

}