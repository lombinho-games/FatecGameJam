using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PistaList", menuName = "Conteúdo/Lista de pistas")]

public class PistaList : ScriptableObject
{

    [System.Serializable]
    public struct NamedPista {
        public string name;
        public ScriptablePista pista;
    }
    public NamedPista[] pistas;

    public string FindKey(ScriptablePista pista){
        for(int i = 0; i < pistas.Length; i ++){
            if(pistas[i].pista == pista){
                return pistas[i].name;
            }
        }
        return null;
    }

    public ScriptablePista FindValue(string name){
        for(int i = 0; i < pistas.Length; i ++){
            if(pistas[i].name == name){
                return pistas[i].pista;
            }
        }
        return null;
    }
   
}
