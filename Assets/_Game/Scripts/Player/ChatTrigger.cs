using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    [TextArea(2, 4)]
    public string chatMessage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IgnoreParentScale boxChat = other.GetComponentInChildren<IgnoreParentScale>();
            if (boxChat != null)
            {
                boxChat.ShowMessage(chatMessage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IgnoreParentScale boxChat = other.GetComponentInChildren<IgnoreParentScale>();
            if (boxChat != null)
            {
                boxChat.Hide();
                boxChat.OnHidden += () =>
                {
                    Destroy(gameObject);
                    boxChat.OnHidden = null; 
                };
            } 
        }
    }
}
