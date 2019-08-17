using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pista", menuName = "Conteúdo/Pista")]
public class ScriptablePista : ScriptableObject
{
    
    public Dialogo dialogo;
    public Sprite image;
    public string displayName;
    public string itemId;

    public List<string> messages;

    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;

}
