using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class PopulateSlotMenu : MonoBehaviour
{

    public FadeEffect fadeEffect;
    public GameObject newCasePrefab;
    public GameObject slotsPrefab;
    public GameObject slotsGroup;

    [Header("Page navigation")]

    public Button previousPage;
    public Button nextPage;
    public Text pageText;


    int curr_page = 0;
    const int SLOTS_PER_PAGE = 4;

    List<List<SlotUI>> pages;

    // Start is called before the first frame update
    void Start()
    {
        pages = new List<List<SlotUI>>();
        pages.Add(new List<SlotUI>());

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
            //slotGO.transform.SetAsFirstSibling();

            SlotUI ui = slotGO.GetComponent<SlotUI>();
            ui.id = slot.id;
            ui.populate = this;
            ui.dataInicio = slot.date;
            ui.cenario = slot.scenario;
            ui.tempoDeJogo = slot.gameTime;
            ui.fadeEffect = fadeEffect;

            AddToPage(ui);
            ui.gameObject.SetActive(false);
        }

        LoadPage(0);

    }

    public void NextPage(){
        if(curr_page < pages.Count - 1){
            curr_page++;
            LoadPage(curr_page);
        }
    }

    public void PreviousPage(){
        if(curr_page > 0){
            curr_page--;
            LoadPage(curr_page);
        }
    }

    public void LoadPage(int page){
        for(int i = 0; i < pages.Count; i ++){
            for(int j = 0; j < pages[i].Count; j ++){
                pages[i][j].gameObject.SetActive(i == page);
            }
        }
    }

    public void RemovePage(SlotUI slot){
        for(int i = 0; i < pages.Count; i ++){
            if(pages[i].Contains(slot)){
                pages[i].Remove(slot);
            }
        }
        CorrectPageAfterDelete();
    }

    public void AddToPage(SlotUI slot){

        for(int i = 0; i < pages.Count; i ++){
            List<SlotUI> pg = pages[i];

            if(pg.Count < SLOTS_PER_PAGE){
                pg.Add(slot);
                return;
            }
        }

        List<SlotUI> page = new List<SlotUI>();
        page.Add(slot);
        pages.Add(page);


    }

    // Update is called once per frame
    void Update()
    {
        pageText.text = (curr_page+1) + "/" + pages.Count;

        previousPage.interactable = curr_page > 0;
        nextPage.interactable = curr_page < pages.Count-1;
    }

    public void CreateNewCase(){
        Debug.Log("Creating new Case");

        Slot newSlot = new Slot(GlobalProfile.gameSlots.NextID(), 3/* ID do Hall */, DateTime.Now, new DateTime());
        GlobalProfile.gameSlots.AddSlotToList(newSlot);
        SaveGameSystem.SaveGame(GlobalProfile.gameSlots, "slots");

        GameObject slotGO = Instantiate(slotsPrefab);
        slotGO.transform.SetParent(slotsGroup.transform, false);
        //slotGO.transform.SetAsFirstSibling();

        SlotUI ui = slotGO.GetComponent<SlotUI>();
        ui.populate = this;
        ui.id = newSlot.id;
        ui.dataInicio = newSlot.date;
        ui.cenario = newSlot.scenario;
        ui.tempoDeJogo = newSlot.gameTime;
        ui.fadeEffect = fadeEffect;
        ui.gameObject.SetActive(false);

        AddToPage(ui);

        curr_page = pages.Count - 1;

        LoadPage(curr_page);
    }

    public SlotUI GetLast(List<SlotUI> list){
        return list[list.Count-1];
    }

    public void CorrectPageAfterDelete(){
        //se uma pagina que não seja a ultima não tiver cheia, faz um shift entre todas as paginas
        for(int i = pages.Count - 2; i >= 0; i --){
            while(pages[i].Count != SLOTS_PER_PAGE){
                for(int j = i; j < pages.Count-1; j ++){
                    Debug.Log("Shifting first item of page " + (j+1) + " and adding to page " + j);
                    pages[j].Add( pages[j+1][0] );
                    pages[j+1].RemoveAt(0);
                }
            }
        }

        //se a ultima pagina estiver vazia, exclui ela, exceto se ela for a unica
        while(pages[pages.Count-1].Count == 0 && pages.Count != 1){
            pages.RemoveAt(pages.Count - 1);
        }

        while(curr_page >= pages.Count){
            curr_page --;
        }
        LoadPage(curr_page);
    }
}
