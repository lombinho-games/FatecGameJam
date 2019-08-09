using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalProfile
{
    static GlobalProfile instance;
    public static GlobalProfile getInstance(){
        if(instance == null){
            instance = new GlobalProfile();
        }
        return instance;
    }
    private GlobalProfile(){}


    //Instance methods
    List<InventoryItem> items;

    public void addItem(InventoryItem item){
        if(items == null){
            items = new List<InventoryItem>();
        }

        items.Add(item);
    }

    public List<InventoryItem> GetItems(){
        if(items == null) items = new List<InventoryItem>();
        return items;
    }

}
