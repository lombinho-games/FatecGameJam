using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedMessages : SaveGame
{

    [SerializeField]
    List<string> messages;

    public SerializedMessages()
    {
        messages = new List<string>();
    }
    public void SendMessage(string message)
    {
        if (!messages.Contains(message)) {
            messages.Add(message);
        }
    }

    public bool Contains(string message)
    {
        return messages.Contains(message);
    }

}
    
