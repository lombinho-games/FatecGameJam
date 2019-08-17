using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterList", menuName = "Conteúdo/Lista de personagens")]
public class CharacterList : ScriptableObject
{
    [System.Serializable]
    public struct NamedCharacter {
        public string name;
        public ScriptableCharacter personagem;
    }
    public NamedCharacter[] personagens;

    public string FindKey(ScriptableCharacter personagem){
        for(int i = 0; i < personagens.Length; i ++){
            if(personagens[i].personagem == personagem){
                return personagens[i].name;
            }
        }
        return null;
    }

    public ScriptableCharacter FindValue(string name){
        for(int i = 0; i < personagens.Length; i ++){
            if(personagens[i].name == name){
                return personagens[i].personagem;
            }
        }
        return null;
    }
}
