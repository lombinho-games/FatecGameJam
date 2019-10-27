using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotsData : SaveGame
{
    [SerializeField]

    public Dictionary<int, Slot> Slots;

    public SlotsData(){
        Slots = new Dictionary<int, Slot>();
    }

    public void AddSlotToList(Slot slot){
        Slots.Add(slot.id, slot);
    }

    public int NextID(){

        int last = lastID();
        for(int i = 0; i < last; i ++){
            if(!Slots.ContainsKey(i)){
                return i;
            }
        }

        return last + 1;

    }

    public int lastID(){
        int id = -1;

        foreach(int key in Slots.Keys){
            Slot slot = Slots[key];
            if(slot.id > id){
                id = slot.id;
            }
        }

        return id;
    }
}
