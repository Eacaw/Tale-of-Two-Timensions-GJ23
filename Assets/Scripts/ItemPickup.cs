using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Light hoverIndicatorLight;
    public CanvasRenderer canvasRenderer; // Maybe don't need anymore
    private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        hoverIndicatorLight.intensity = 0;

        // Hide the GUI icon for this item
        canvasRenderer.SetAlpha(0); // Shouldn't need this anymore

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    public void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 6.0f)
        {
            Debug.Log("Too far away to pick up item");
            return;
        }

        if (gameObject.CompareTag("Backpack"))
        {
            playerController.SetBackpackGUI(100);
            playerController.hasBackpack = true;
        }

        if (!playerController.hasBackpack)
        {
            Debug.Log("You need a backpack to pick up items");
            return;
        }

        // Pickup objects and update player controller
        if (gameObject.CompareTag("Key"))
        {
            playerController.hasKey = true;
            playerController.SetKeyGUI(100);
            playerController.currentCheckpoint = 9;
        }

        if (gameObject.CompareTag("Amulet"))
        {
            playerController.hasAmulet = true;
            playerController.SetAmuletGUI(100);
            playerController.currentCheckpoint = 2;
        }

         if (gameObject.CompareTag("Poison"))
        {
            playerController.SetPoisonGUI(100);
            playerController.currentCheckpoint = 4;
        }

         if (gameObject.CompareTag("WizardJuice"))
        {
            playerController.SetWizardJuicGUI(100); // Maybe?
            playerController.currentCheckpoint = 10;
        }

        player.GetComponent<Animator>().SetTrigger("pickupItem");
        canvasRenderer.SetAlpha(100);
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
}
