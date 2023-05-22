using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubbleController : MonoBehaviour
{
    public void CloseUI() 
    {
        this.gameObject.SetActive(false);
    }
}
