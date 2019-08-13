using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenarioData : SaveGame
{
    [SerializeField]
    public List<CharacterData> characters;
    [SerializeField]
    public List<PistaData> pistas;
    [SerializeField]
    public List<ExitData> exits;

    public ScenarioData(){
        characters = new List<CharacterData>();
        pistas = new List<PistaData>();
        exits = new List<ExitData>();
    }

}
