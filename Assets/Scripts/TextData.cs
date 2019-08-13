using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class TextData
{
     [TextArea(3,10)]
    public string texto;

    public string imageUrl;
    [System.NonSerialized]
    private Sprite imageSprite;

    public Sprite image{
        get{
            Debug.Log("Loading image resource " + imageUrl);
            if(imageSprite == null){
                imageSprite = Resources.Load<Sprite>(imageUrl);
            }
            return imageSprite;
        }
        set {
            imageSprite = value;
            if(value != null){
                imageUrl = AssetDatabase.GetAssetPath(value).Split(new string[]{"Resources"}, System.StringSplitOptions.None)[1].Split('.')[0].Substring(1);
            }
            else{
                imageUrl = null;
            }
        }
    }

    public string owner;
}
