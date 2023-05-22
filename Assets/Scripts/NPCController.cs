using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float detectionRadius = 5f;
    private Transform player;

    public Light npcIndicatorLight;

    public string npcName = "NPC";

    public GameObject npcChatUI;

    private void Start()
    {
        npcChatUI.SetActive(false);
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

            Quaternion targetRotation = Quaternion.LookRotation(
                targetPosition - transform.position
            );
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * 5f
            );
        }
    }

    public void OnMouseDown()
    {
        npcChatUI.SetActive(true);
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
