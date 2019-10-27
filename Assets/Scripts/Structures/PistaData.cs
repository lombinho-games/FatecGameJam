using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PistaData
{
    
    public Dialogo dialogo;
    public string image; //ID no manager
    public string displayName;
    public string itemId;
    public string itemDescription;
    public List<string> messages;

    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;
}
