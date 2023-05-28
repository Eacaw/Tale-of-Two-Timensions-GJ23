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
            Debug.Log("showing skeleton");
            this.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("hiding skeleton");
            
            this.gameObject.SetActive(false);
        }
    }
}
