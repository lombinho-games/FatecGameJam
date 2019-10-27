using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string itemID;
    public string displayName;
    public Sprite image;
    public string description;

    public InventoryItem(string itemID, string displayName, Sprite image, string description){
        this.itemID = itemID;
        this.displayName = displayName;
        this.image = image;
        this.description = description;
    }
}
