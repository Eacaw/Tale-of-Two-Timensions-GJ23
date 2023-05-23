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
        DisplayNPCName();
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

    private void DisplayNPCName()
    {
        GameObject nameText = new GameObject("NPC Name");
        nameText.transform.SetParent(transform);
        nameText.transform.position = transform.position + new Vector3(0, 4f, 0);
        TextMesh textMesh = nameText.AddComponent<TextMesh>();
        textMesh.text = npcName;
        textMesh.fontSize = 50;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        nameText.transform.rotation =
            Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 180, 0);
        nameText.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        textMesh.color = Color.grey;
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
