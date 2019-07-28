using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelected : MonoBehaviour
{

    GameObject slot;
    public GameObject originalItem;

    public Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mpos = myCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mpos.x, mpos.y, 0);


        if(Input.GetMouseButtonUp(0)){ //soltei o btao
            if(slot != null){ //num slot
                if(originalItem.GetComponent<DragDrop>() != null){ //do inventário
                    if(slot.GetComponent<SlotHolder>().held == null){ //num slot que nao tem item dentro
                        Debug.Log("num slot do inventário pra um slot q nao tem item dentro");
                        GameObject copy = Instantiate(originalItem);
                        copy.transform.SetParent(slot.gameObject.transform);
                        copy.SetActive(true);
                        Destroy(copy.GetComponent<DragDrop>());
                        DragDropOnSlot ddos = copy.AddComponent<DragDropOnSlot>();
                        ddos.refreshEventTrigger();
                        ddos.itemSelected = this;
                        ddos.slot = slot.GetComponent<SlotHolder>();
                        ddos.originalItem = originalItem.GetComponent<DragDrop>();

                        copy.transform.position = new Vector3(copy.transform.position.x, copy.transform.position.y, 0);

                        RectTransform rect = copy.GetComponent<RectTransform>();

                        rect.anchorMin = new Vector2(0, 0);
                        rect.anchorMax = new Vector2(1, 1);
                        rect.pivot = new Vector2(0.5f, 0.5f);

                        rect.offsetMin = new Vector2(10, 10);
                        rect.offsetMax = new Vector2(-10, -10);

                        rect.localScale = new Vector3(.8f, .8f, 1);

                        gameObject.SetActive(false);

                        slot.GetComponent<SlotHolder>().held = originalItem.GetComponent<DragDrop>();
                    }
                    else{ //num slot que tem item dentro
                        Debug.Log("num slot do inventário pra um slot q tem item dentro");
                        originalItem.SetActive(true);
                        gameObject.SetActive(false);
                    }

                }
                else if(originalItem.GetComponent<DragDropOnSlot>() != null){ //de um slot

                    if(originalItem.GetComponent<DragDropOnSlot>().slot == slot){ //do mesmo slot
                        Debug.Log("num slot de um slot (o mesmo slot)");
                        originalItem.gameObject.SetActive(true);
                        gameObject.SetActive(false);
                    }
                    else{ //pra outro slot

                        if(slot.GetComponent<SlotHolder>().held == null){ // que está vazio
                            Debug.Log("de um slot para outro slot que está vazio");
                            originalItem.GetComponent<DragDropOnSlot>().slot.held = null;
                            GameObject copy = Instantiate(originalItem);
                            copy.transform.SetParent(slot.gameObject.transform);
                            copy.SetActive(true);
                            copy.GetComponent<DragDropOnSlot>().slot = slot.GetComponent<SlotHolder>();

                            copy.GetComponent<DragDropOnSlot>().refreshEventTrigger();

                            RectTransform rect = copy.GetComponent<RectTransform>();

                            rect.anchorMin = new Vector2(0, 0);
                            rect.anchorMax = new Vector2(1, 1);
                            rect.pivot = new Vector2(0.5f, 0.5f);

                            rect.offsetMin = new Vector2(10, 10);
                            rect.offsetMax = new Vector2(-10, -10);

                            rect.localScale = new Vector3(.8f, .8f, 1);

                            copy.transform.position = new Vector3(copy.transform.position.x, copy.transform.position.y, 0);

                            gameObject.SetActive(false);

                            Destroy(originalItem.gameObject);

                            slot.GetComponent<SlotHolder>().held = copy.GetComponent<DragDropOnSlot>().originalItem;

                        }
                        else{//que tem um item dentro
                            Debug.Log("De um slot para outro slot que tem item dentro");
                            originalItem.gameObject.SetActive(true);
                            gameObject.SetActive(false);
                        }
                    }
                }
            }
            else{ //fora de slot
                if(originalItem.GetComponent<DragDrop>() != null){ //do inventário
                    Debug.Log("do inventário para fora de um slot");
                    originalItem.SetActive(true);
                    gameObject.SetActive(false);
                }
                else if(originalItem.GetComponent<DragDropOnSlot>() != null){ // de um slot
                    Debug.Log("de um slot para fora de um slot");
                    gameObject.SetActive(false);
                    originalItem.GetComponent<DragDropOnSlot>().slot.held = null;
                    originalItem.GetComponent<DragDropOnSlot>().originalItem.gameObject.SetActive(true);
                    Destroy(originalItem);
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Slot"){
            slot = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        slot = null;
    }


}
