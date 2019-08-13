using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class CharacterData
{
    [SerializeField]
    public List<Dialogo> dialogos;


    public string defaultImageUrl;
    [System.NonSerialized]
    private Sprite defaultImageSprite;
    public Sprite defaultImage{
        get{
            if(defaultImageSprite == null){
                defaultImageSprite = Resources.Load<Sprite>(defaultImageUrl);
            }
            return defaultImageSprite;
        }
        set {
            defaultImageSprite = value;
            if(value != null){
                defaultImageUrl = AssetDatabase.GetAssetPath(value).Split(new string[]{"Resources"}, System.StringSplitOptions.None)[1].Split('.')[0].Substring(1);
            }
            else{
                defaultImageUrl = null;
            }
        }
    }


    public string headBobUrl;
    [System.NonSerialized]
    private Sprite headBobSprite;

    public Sprite headBob{
        get{
            if(headBobSprite == null){
                headBobSprite = Resources.Load<Sprite>(headBobUrl);
            }
            return headBobSprite;
        }
        set {
            headBobSprite = value;
            if(value != null){
                headBobUrl = AssetDatabase.GetAssetPath(value).Split(new string[]{"Resources"}, System.StringSplitOptions.None)[1].Split('.')[0].Substring(1);
            }
            else{
                headBobUrl = null;
            }
        }
    }

    //Transform
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;

    public CharacterData(){
        dialogos = new List<Dialogo>();
    }

}
