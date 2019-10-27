using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class TextData
{
     [TextArea(3,10)]
    public string texto;
    public string image;
    public string owner;
}
