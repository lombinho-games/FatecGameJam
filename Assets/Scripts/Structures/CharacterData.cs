using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    
    //Propriedades
    public string displayName;
    public string id;
    public string description;
    public string defaultImage; //ID da imagem
    public string headBob; //ID da imagem
    bool hasTalked;

    [SerializeField]
    public List<Dialogo> dialogos;

    //Transform

    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;

}
