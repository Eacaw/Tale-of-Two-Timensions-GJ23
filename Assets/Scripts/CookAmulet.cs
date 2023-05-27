using UnityEngine;

public class CookAmulet : MonoBehaviour
{
    public Light indicatorLight;
    public GameObject LiquidInPot;
    public GameObject AmuletInPot;

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
        GameObject.FindGameObjectsWithTag("Player")[0]
            .GetComponent<PlayerController>()
            .currentCheckpoint = 11;
    }

    private void OnMouseEnter()
    {
        indicatorLight.intensity = 5;
    }

    private void OnMouseExit()
    {
        indicatorLight.intensity = 0;
    }
}
