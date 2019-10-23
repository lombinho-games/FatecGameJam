using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PopulateSlotMenu : MonoBehaviour
{

    public GameObject newCasePrefab;
    public GameObject slotsPrefab;
    public GameObject slotsGroup;
    // Start is called before the first frame update
    void Start()
    {
        if(SaveGameSystem.DoesSaveGameExist("slots")){
            GlobalProfile.gameSlots = (SlotsData)SaveGameSystem.LoadGame("slots");
        }
        else{
            GlobalProfile.gameSlots = new SlotsData();
            SaveGameSystem.SaveGame(GlobalProfile.gameSlots, "slots");
        }

        foreach(int key in GlobalProfile.gameSlots.Slots.Keys){
            Slot slot = GlobalProfile.gameSlots.Slots[key];

            GameObject slotGO = Instantiate(slotsPrefab);
            slotGO.transform.SetParent(slotsGroup.transform, false);
            slotGO.transform.SetAsFirstSibling();

            SlotUI ui = slotGO.GetComponent<SlotUI>();
            ui.id = slot.id;
            ui.dataInicio = slot.date;
            ui.cenario = slot.scenario;
            ui.tempoDeJogo = slot.gameTime;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNewCase(){
        Debug.Log("Creating new Case");

        Slot newSlot = new Slot(GlobalProfile.gameSlots.NextID(), 2/* ID do Hall */, DateTime.Now, new DateTime());
        GlobalProfile.gameSlots.AddSlotToList(newSlot);
        SaveGameSystem.SaveGame(GlobalProfile.gameSlots, "slots");

        GameObject slotGO = Instantiate(slotsPrefab);
        slotGO.transform.SetParent(slotsGroup.transform, false);
        slotGO.transform.SetAsFirstSibling();

        SlotUI ui = slotGO.GetComponent<SlotUI>();
        ui.id = newSlot.id;
        ui.dataInicio = newSlot.date;
        ui.cenario = newSlot.scenario;
        ui.tempoDeJogo = newSlot.gameTime;
    }
}
