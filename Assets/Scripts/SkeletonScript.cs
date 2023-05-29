using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    void Start()
    {
        PlayerController playerController = GameObject.FindGameObjectsWithTag("Player")[
            0
        ].GetComponent<PlayerController>();
        if (playerController.hasKilledBlacksmith)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
