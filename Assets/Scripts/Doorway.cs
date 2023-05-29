using UnityEngine;
using UnityEngine.EventSystems;

public class Doorway : MonoBehaviour, IPointerClickHandler
{
    public Light hoverIndicatorLight;
    public Transform destination;
    public bool isLocked = false;
    [SerializeField] private FMODUnity.EventReference DoorEventPath;

    private void Start()
    {
        hoverIndicatorLight.intensity = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
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
        hoverIndicatorLight.intensity = 12;
    }

    public void OnMouseExit()
    {
        hoverIndicatorLight.intensity = 0;
    }

    void PlayDoor()
    {
        FMOD.Studio.EventInstance door = FMODUnity.RuntimeManager.CreateInstance(DoorEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(door, transform, GetComponent<Rigidbody>());

        door.start();
        door.release();
    }
}
