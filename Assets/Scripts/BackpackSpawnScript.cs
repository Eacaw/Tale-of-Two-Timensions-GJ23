using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletSpawnScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController playerController = GameObject.FindGameObjectsWithTag("Player")[0]
            .GetComponent<PlayerController>();

        if (playerController.hasBackpack)
        {
            this.gameObject.SetActive(false);
        }
    }
}
