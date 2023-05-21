using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float detectionRadius = 5f;
    private Transform player;

    public Light npcIndicatorLight;

    public string npcName = "NPC";

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        npcIndicatorLight.intensity = 0;
    }

    private void Update()
    {
         if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
           LookAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            Vector3 targetPosition = player.position;
            targetPosition.y = transform.position.y; // Ignore vertical difference

            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("Player clicked on " + npcName + "!");
    }

    public void OnMouseEnter()
    {
        npcIndicatorLight.intensity = 5;
    }

    public void OnMouseExit()
    {
        npcIndicatorLight.intensity = 0;
    }
}
