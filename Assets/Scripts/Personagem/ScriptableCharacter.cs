using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Conteúdo/Personagem")]
public class ScriptableCharacter : ScriptableObject
{
    public List<Dialogo> dialogos;
    public Sprite defaultImage;
    public Sprite headBob;

    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;

}
