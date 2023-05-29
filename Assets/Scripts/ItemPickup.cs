using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickup : MonoBehaviour, IPointerClickHandler
{
    public Light hoverIndicatorLight;
    [SerializeField] private FMODUnity.EventReference ItemPickupEventPath;
    [SerializeField] private string ItemParameterName;
    private GameObject player;
    private PlayerController playerController;

    private DialogueTrigger[] dialogItems;

    private void Start()
    {
        hoverIndicatorLight.intensity = 0;

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        dialogItems = this.gameObject.GetComponents<DialogueTrigger>();

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
        if (Vector3.Distance(transform.position, player.transform.position) > 6.0f)
        {
            dialogItems[1].TriggerDialogue();
            return;
        }

        if (gameObject.CompareTag("Backpack"))
        {
            playerController.SetBackpackGUI(true);
            playerController.hasBackpack = true;
            PlayItemPickup(0f);
        }

        if (!playerController.hasBackpack)
        {
            dialogItems[0].TriggerDialogue();
            return;
        }

        // Pickup objects and update player controller
        if (gameObject.CompareTag("Key"))
        {
            playerController.hasKey = true;
            playerController.SetKeyGUI(true);
            playerController.currentCheckpoint = 9;
            PlayItemPickup(2f);
        }

        if (gameObject.CompareTag("Amulet"))
        {
            playerController.hasAmulet = true;
            playerController.SetAmuletGUI(true);
            playerController.currentCheckpoint = 2;
            PlayItemPickup(1f);
        }

        if (gameObject.CompareTag("Poison"))
        {
            playerController.SetPoisonGUI(true);
            playerController.currentCheckpoint = 4;
            PlayItemPickup(3f);
        }

        if (gameObject.CompareTag("WizardJuice"))
        {
            playerController.SetWizardJuicGUI(true); // Maybe?
            playerController.currentCheckpoint = 10;
            PlayItemPickup(3f);
        }

        player.GetComponent<Animator>().SetTrigger("pickupItem");
        Destroy(gameObject);
    }

    public void OnMouseEnter()
    {
        hoverIndicatorLight.intensity = 10;
    }

    public void OnMouseExit()
    {
        hoverIndicatorLight.intensity = 0;
    }

    void PlayItemPickup(float parameter) {
        FMOD.Studio.EventInstance pickup = FMODUnity.RuntimeManager.CreateInstance(ItemPickupEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pickup, transform, GetComponent<Rigidbody>());

        pickup.setParameterByName(ItemParameterName, parameter);
        pickup.start();
        pickup.release();
    }
}
