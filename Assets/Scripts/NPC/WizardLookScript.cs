using UnityEngine;

public class WizardLookScript : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.LookAt(player);
    }
}