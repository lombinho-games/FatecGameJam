﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalProfile
{
    static GlobalProfile instance;
    static bool dirty = false;
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

        if (!items.Contains(item)) {
            dirty = true;
            items.Add(item);
        }
    }

    public List<InventoryItem> GetItems(TextureManager manager){
        if (!dirty) {
            LoadInventory(manager);
        }

        if(items == null) items = new List<InventoryItem>();
        return items;
    }

    public void LoadInventory(TextureManager manager)
    {
        dirty = false;
        InventorySave inventory = SaveGameSystem.LoadGame("slot0_inventory") as InventorySave;
        if (inventory == null) return;

        if (items == null) items = new List<InventoryItem>();

        items.Clear();
        for(int i = 0; i < inventory.displayName.Count; i++) {
            items.Add(
                new InventoryItem(
                    inventory.items[i],
                    inventory.displayName[i],
                    manager.GetSpritePista(inventory.items[i]),
                    inventory.descriptions[i]
                    )
                );
        }
    }

    public void SaveInventory()
    {
        dirty = false;
        SaveGameSystem.SaveGame(GetSerializableInventory(), "slot0_inventory");
    }

    public InventorySave GetSerializableInventory()
    {
        if (items == null) items = new List<InventoryItem>();
        InventorySave inventory = new InventorySave();
        foreach(InventoryItem i in items) {
            inventory.items.Add(i.itemID);
            inventory.displayName.Add(i.displayName);
            inventory.descriptions.Add(i.description);
        }

        return inventory;
    }

    [System.Serializable]
    public class InventorySave : SaveGame
    {
        [SerializeField]
        public List<string> items;
        [SerializeField]
        public List<string> displayName;
        [SerializeField]
        public List<string> descriptions;

        public InventorySave()
        {
            items = new List<string>();
            displayName = new List<string>();
            descriptions = new List<string>();
        }
    }

}
