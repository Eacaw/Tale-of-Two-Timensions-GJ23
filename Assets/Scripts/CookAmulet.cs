using UnityEngine;

public class CookAmulet : MonoBehaviour
{
    public Light indicatorLight;
    public GameObject LiquidInPot;
    public GameObject AmuletInPot;
    private bool hasCookedAmulet = false;

    private void Start()
    {
        indicatorLight.intensity = 0;
        LiquidInPot.SetActive(false);
        AmuletInPot.SetActive(false);
    }

    private void OnMouseDown()
    {
        LiquidInPot.SetActive(true);
        AmuletInPot.SetActive(true);

        PlayerController playerController = GameObject.FindGameObjectsWithTag("Player")[
            0
        ].GetComponent<PlayerController>();

        playerController.currentCheckpoint = 11;
        playerController.SetWizardJuicGUI(false);
        playerController.SetAmuletGUI(false);
        this.hasCookedAmulet = true;
    }

    private void OnMouseEnter()
    {
        if (!this.hasCookedAmulet)
        {
            indicatorLight.intensity = 5;
        }
    }

    private void OnMouseExit()
    {
        indicatorLight.intensity = 0;
    }
}
