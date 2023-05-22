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
                if (hit.collider.CompareTag("Backpack"))
                {
                    BackpackPickup backpack = hit.collider.GetComponent<BackpackPickup>();
                    if (backpack != null)
                    {
                        backpack.OnMouseDown();
                    }
                }
            }
        }
    }
}
