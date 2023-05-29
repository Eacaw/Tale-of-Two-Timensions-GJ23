using UnityEngine;
using UnityEngine.EventSystems;

public class PrisonCellDialogTriggers : MonoBehaviour, IPointerClickHandler
{
    public Light npcIndicatorLight;

    public void Start()
    {
        npcIndicatorLight.intensity = 0;
    }

    public void OnMouseEnter()
    {
        npcIndicatorLight.intensity = 5;
    }

    public void OnMouseExit()
    {
        npcIndicatorLight.intensity = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.gameObject.GetComponents<DialogueTrigger>()[0].TriggerDialogue();
    }
}
