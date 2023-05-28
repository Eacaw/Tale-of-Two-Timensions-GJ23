using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    public Light hoverIndicatorLight;
    public Transform destination;
    public bool isLocked = false;

    private void Start()
    {
        hoverIndicatorLight.intensity = 0;
    }

    public void OnMouseDown()
    {
        DialogueTrigger dialogueTrigger = this.gameObject.GetComponent<DialogueTrigger>();
        if (
            dialogueTrigger != null
            && this.isLocked == true
            && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().hasKey
                == false
        )
        {
            dialogueTrigger.TriggerDialogue();
        }

        if (
            isLocked
            && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().hasKey
                == false
        )
        {
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = destination.position;
    }

    public void OnMouseEnter()
    {
        hoverIndicatorLight.intensity = 5;
    }

    public void OnMouseExit()
    {
        hoverIndicatorLight.intensity = 0;
    }
}
