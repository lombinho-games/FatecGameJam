using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenarioData : SaveGame
{
    [SerializeField]
    public List<string> characters;
    [SerializeField]
    public List<string> pistas;

    
    //[SerializeField]
    //public List<ExitData> exits;
    public ScenarioData(){
        characters = new List<string>();
        pistas = new List<string>();
        //exits = new List<ExitData>();
    }

}
