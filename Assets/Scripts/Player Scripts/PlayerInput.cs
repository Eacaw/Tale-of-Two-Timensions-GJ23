using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("NPC"))
                {
                    NPCController npc = hit.collider.GetComponent<NPCController>();
                    if (npc != null)
                    {
                        npc.OnMouseDown();
                    }
                }
            }
        }
    }
}
