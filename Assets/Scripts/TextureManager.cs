using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextureManager", menuName = "Texture Manager")]
public class TextureManager : ScriptableObject
{
   [System.Serializable]
   public struct NamedSprite{
       public string name;
       public Sprite sprite;
   }
   public NamedSprite[] sprites;
   public NamedSprite[] pistas;

   public string[] getPoseKeys(){
       string[] keys = new string[sprites.Length];
       for(int i = 0; i < keys.Length; i ++){
           keys[i] = sprites[i].name;
       }
       return keys;
   }
   public string[] getPistaKeys(){
       string[] keys = new string[pistas.Length];
       for(int i = 0; i < keys.Length; i ++){
           keys[i] = pistas[i].name;
       }
       return keys;
   }

   public int getIndexByKeyPose(string key){
       for(int i = 0; i < sprites.Length; i ++){
           if(sprites[i].name == key){
               return i;
           }
       }
       return 0;
   }

   public int getIndexByKeyPista(string key){
       for(int i = 0; i < pistas.Length; i ++){
           if(pistas[i].name == key){
               return i;
           }
       }
       return 0;
   }

   public Sprite GetSpritePose(string value){
       int val = getIndexByKeyPose(value);
       return val != -1 ? sprites[val].sprite : null;
   }

   public Sprite GetSpritePista(string value){
       int val = getIndexByKeyPista(value);
       return val != -1 ? pistas[val].sprite : null;
   }
}
