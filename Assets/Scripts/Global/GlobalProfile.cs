using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalProfile
{
    public static int Slot = 0;
    public static SlotsData gameSlots;
    static GlobalProfile instance;
    static bool dirty = false;
    public int lastScenarioBeforeInventory = 0;
    public List<TextData> dialogIgnition;

    public static GlobalProfile getInstance(){
        if(instance == null){
            instance = new GlobalProfile();
        }
        return instance;
    }
    private GlobalProfile(){
        items = new List<InventoryItem>();
        messages = new SerializedMessages();
    }

    //Instance methods
    List<InventoryItem> items;
    SerializedMessages messages;

    public bool HasReceivedMessage(string message)
    {
        return messages.Contains(message);
    }

    public void SendMessage(string message)
    {
        if (message.Length == 0) return;

        messages.SendMessage(message);

        InspectionManager manager = (InspectionManager)GameObject.FindObjectOfType(typeof(InspectionManager));
        if(manager == null) return;

        manager.PropagateMessage(message);
    }

    public void addItem(InventoryItem item){
        if(items == null){
            items = new List<InventoryItem>();
        }

        if (!ContainsItem(item.itemID)) {
            dirty = true;
            items.Add(item);
        }
    }

    public bool ContainsItem(string id){
        for(int i = 0; i < items.Count; i ++){
            if(items[i].itemID == id){
                return true;
            }
        }
        return false;
    }

    public List<InventoryItem> GetItems(TextureManager manager){
        if (!dirty) {
            LoadInventory(manager);
        }

        if(items == null) items = new List<InventoryItem>();
        return items;
    }

    public void SaveGame()
    {
        SaveGameSystem.SaveGame(messages, "slot"+Slot+"_messages");
        gameSlots.Slots[Slot].scenario = SceneManager.GetActiveScene().buildIndex;
        SaveGameSystem.SaveGame(gameSlots, "slots");
        SaveInventory();
    }

    public static string GetSceneNameByPath(string sceneName){
        string[] ped = sceneName.Split(new char[] {'/'});
        return ped[ped.Length-1].Split(new char[] {'.'})[0];
    }

    public static string GetCurrentSceneName(){
        return GetSceneNameByPath(SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadGame(TextureManager manager)
    {
        if (SaveGameSystem.DoesSaveGameExist("slot"+Slot+"_messages")) {
            messages = SaveGameSystem.LoadGame("slot"+Slot+"_messages") as SerializedMessages;
        }
        LoadInventory(manager);
    }

    void LoadInventory(TextureManager manager)
    {
        dirty = false;
        InventorySave inventory = SaveGameSystem.LoadGame("slot"+Slot+"_inventory") as InventorySave;
        if (inventory == null) return;

        if (items == null) items = new List<InventoryItem>();

        items.Clear();
        for(int i = 0; i < inventory.displayName.Count; i++) {
            Sprite sp = manager.GetSpritePista(inventory.items[i]);
            items.Add(
                new InventoryItem(
                    inventory.items[i],
                    inventory.displayName[i],
                    sp,
                    inventory.descriptions[i]
                    )
                );
        }
    }

    public void SaveInventory()
    {
        dirty = false;
        SaveGameSystem.SaveGame(GetSerializableInventory(), "slot"+Slot+"_inventory");
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
