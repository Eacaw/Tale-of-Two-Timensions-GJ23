using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSpawnScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController playerController = GameObject.FindGameObjectsWithTag("Player")[0]
            .GetComponent<PlayerController>();

        if (playerController.hasAmulet)
        {
            this.gameObject.SetActive(false);
        }
    }
}
