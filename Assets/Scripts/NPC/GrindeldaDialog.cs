using UnityEngine;
using UnityEngine.EventSystems;

public class GrindeldaDialog : MonoBehaviour, IPointerClickHandler
{
    private DialogueTrigger[] dialogItems;
    public Light npcIndicatorLight;

    void Start()
    {
        dialogItems = this.gameObject.GetComponents<DialogueTrigger>();
 DisplayNPCName();
        npcIndicatorLight.intensity = 0;

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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int currentCheckpoint = GameObject.FindGameObjectsWithTag("Player")[0]
            .GetComponent<PlayerController>()
            .currentCheckpoint;
        if (currentCheckpoint < 4)
        {
            dialogItems[0].TriggerDialogue();
        }
        else if (currentCheckpoint == 4 || currentCheckpoint == 5)
        {
            dialogItems[1].TriggerDialogue();
            this.gameObject.GetComponent<GrindeldaPoisonRumScript>().addPoisonToRum();

        }
        else
        {
            dialogItems[2].TriggerDialogue();
        }
    }

    void OnMouseEnter()
    {
        npcIndicatorLight.intensity = 5;
    }

    void OnMouseExit()
    {
        npcIndicatorLight.intensity = 0;
    }

     private void DisplayNPCName()
    {
        GameObject nameText = new GameObject("NPC Name");
        nameText.transform.SetParent(transform);
        nameText.transform.position = transform.position + new Vector3(0, 4f, 0);
        TextMesh textMesh = nameText.AddComponent<TextMesh>();
        textMesh.text = "Grindelda";
        textMesh.fontSize = 50;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        nameText.transform.rotation =
            Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 180, 0);
        nameText.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        textMesh.color = Color.grey;
    }
}
