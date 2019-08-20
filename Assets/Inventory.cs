using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject content;
    public TextureManager manager;
    public Sprite slotSprite;
    // Start is called before the first frame update
    void Start()
    {

        RectTransform rect = GetComponent<RectTransform>();

        int cols = 4;
        float margin = 10;
        float width = (rect.rect.width - (cols+1) * margin) / cols;

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

            GameObject itemGO = new GameObject("Image");
            RectTransform itemRect = itemGO.AddComponent<RectTransform>();
            Image itemImage = itemGO.AddComponent<Image>();
            itemImage.preserveAspect = true;
            itemImage.sprite = item.image;
            itemRect.anchorMin = new Vector2(0, 0);
            itemRect.anchorMax = new Vector2(1, 1);

            itemRect.offsetMin = new Vector2(20, 20);
            itemRect.offsetMax = new Vector2(-20, -20);

            itemGO.transform.SetParent(slot.transform, false);


            x++;
            if(x == 4) {
                x = 0;
                y++;
            }
        }

        content.GetComponent<RectTransform>().offsetMin = new Vector2(0, -(margin + y * (width + margin) + margin));


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
