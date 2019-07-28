using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragDropOnSlot : MonoBehaviour
{   
    public bool Holding;
    public ItemSelected itemSelected;
    public DragDrop originalItem;
    public SlotHolder slot;
    
    void Start () {
        
     }

     public void refreshEventTrigger(){
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { PointerDown(); });
        trigger.triggers.Add(entry);
     }

    void Update()
    {

    }
        
    public void PointerDown(){
        gameObject.SetActive(false);
        itemSelected.gameObject.SetActive(true);
        itemSelected.transform.position = transform.position;
        itemSelected.originalItem = gameObject;
        itemSelected.GetComponent<SpriteRenderer>().sprite = GetComponent<Image>().sprite;

    }
 
}

        
        
    

