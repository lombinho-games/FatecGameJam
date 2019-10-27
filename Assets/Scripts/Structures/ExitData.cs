using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExitData
{
    public string title;
    public int iCursor;
    public int exitPoint;
    public bool enabled;
    public string unlockMessage;

    //Transform
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;
    
}
