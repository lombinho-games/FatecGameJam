using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuadroData : SaveGame
{

    [System.Serializable]
    public struct Item{
        public string name;
        public string id;
        public string description;
        public Vector3 position;
    }

    [System.Serializable]
    public struct Connection{
        public int itemA;
        public int itemB;
        public string connectionName;
    }

    [SerializeField]
    public List<Item> items;

    [SerializeField]
    public List<Connection> connections;

    public QuadroData(){
        items = new List<Item>();
        connections = new List<Connection>();
    }

}
