using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Inventory : MonoBehaviour
{

    public GameObject content;
    public TextureManager manager;
    public Sprite slotSprite;
    public ItemSelection selection;
    public Camera mainCamera;
    public GameObject framePrefab;
    public GameObject pinPrefab;
    Quadro quadro;

    float width;

    // Start is called before the first frame update
    void Start()
    {

        RectTransform rect = GetComponent<RectTransform>();

        int cols = 4;
        float margin = 10;
        width = (rect.rect.width - (cols+1) * margin) / cols;

        int x = 0;
        int y = 0;
        foreach (InventoryItem item in GlobalProfile.getInstance().GetItems(manager)) {

            //Criando o slot
            GameObject slot = new GameObject(item.itemID);
            RectTransform slotRect = slot.AddComponent<RectTransform>();
            Image image = slot.AddComponent<Image>();
            image.sprite = slotSprite;
            image.preserveAspect = true;

            

            slotRect.anchorMin = new Vector2(0, 1);
            slotRect.anchorMax = new Vector2(0, 1);
            slotRect.pivot = new Vector2(0, 1);
            
            slotRect.offsetMin = new Vector2(margin + x*(width+margin), -(margin + width) - y*(margin+width));
            slotRect.offsetMax = new Vector2(margin + x*(width+margin) + width, -margin - y*(margin+width));

            slot.transform.SetParent(content.transform, false);

            //Criando o item dentro do slot

            GameObject itemGO = new GameObject(item.displayName);
            RectTransform itemRect = itemGO.AddComponent<RectTransform>();
            Image itemImage = itemGO.AddComponent<Image>();
            itemImage.preserveAspect = true;
            itemImage.sprite = item.image;

            itemRect.anchorMin = new Vector2(0, 0);
            itemRect.anchorMax = new Vector2(1, 1);

            itemRect.offsetMin = new Vector2(20, 20);
            itemRect.offsetMax = new Vector2(-20, -20);

            itemGO.transform.SetParent(slot.transform, false);

            EventTrigger trigger = itemGO.AddComponent<EventTrigger>();

            //Drag begin
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.BeginDrag;
            entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(BeginDrag));

            trigger.triggers.Add(entry);

            //Drag
            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Drag;
            entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(Drag));

            trigger.triggers.Add(entry);

            //Drop
            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.EndDrag;
            entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(Drop));

            trigger.triggers.Add(entry);


            x++;
            if(x == 4) {
                x = 0;
                y++;
            }
        }

        content.GetComponent<RectTransform>().offsetMin = new Vector2(0, -(margin + y * (width + margin) + margin));


    }

    void BeginDrag(BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData)data;

        if (pointer.pointerDrag.GetComponent<Image>().color.a == 1) {

            pointer.pointerDrag.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            selection.GetComponent<Image>().sprite = pointer.pointerDrag.GetComponent<Image>().sprite;
            selection.name = pointer.pointerDrag.name;

            selection.originalSlot = pointer.pointerDrag;

            selection.gameObject.SetActive(true);
        }
    }

    void Drag(BaseEventData data)
    {
        Debug.Log("Drag");
    }

    void Drop(BaseEventData data)
    {
        PointerEventData pointer = (PointerEventData)data;

        

        if (selection.gameObject.activeInHierarchy) {
            selection.gameObject.SetActive(false);
            if (quadro != null) {
                //Coloca o manolo no quadro
                GameObject itemQuadro = Instantiate(framePrefab);// new GameObject("Item");
                PistaFrame frame = itemQuadro.GetComponent<PistaFrame>();
                
                itemQuadro.transform.Find("Pista").GetComponent<Image>().sprite = selection.GetComponent<Image>().sprite;
                itemQuadro.transform.Find("Text").GetComponent<Text>().text = selection.name;

                //Adiciona evento de click
                EventTrigger trigger = itemQuadro.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>( (BaseEventData baseData) => {

                    if(!frame.draggin)
                        quadro.OpenMenu(frame, selection.originalSlot);

                }));
                trigger.triggers.Add(entry);

                //Posiciona ele
                itemQuadro.transform.SetParent(quadro.content.transform, false);
                itemQuadro.transform.SetAsFirstSibling();
                Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 itPos = mouseWorldPos;
                itPos.z = 0;
                itemQuadro.transform.position = itPos;
                itemQuadro.transform.RotateAround(itemQuadro.transform.position, new Vector3(0, 0, 1), Random.Range(-10, 10));

                GameObject pin = Instantiate(pinPrefab);
                pin.GetComponent<PistaPin>().pista = itemQuadro.transform.Find("Pin").gameObject;
                pin.transform.SetParent(quadro.content.transform, false);
                pin.transform.SetAsLastSibling();

                frame.outerPin = pin;
            }
            else {
                pointer.pointerDrag.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            Vector3 position = selection.transform.position;
            position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            selection.transform.position = position;
        }
    }

    public void PointerEnterQuadro(Quadro quadro)
    {
        this.quadro = quadro;
    }

    public void PointerExitQuadro(Quadro quadro)
    {
        quadro = null;
    }
}
