using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InventoryPopulator : MonoBehaviour
{
    // Start is called before the first frame update

    public int cols;
    public int cellWidth;
    public int cellHeight;

    public ItemSelected itemSelected;
    void Start()
    {
        int ix = 0;
        int iy = 0;
        foreach(InventoryItem item in GlobalProfile.getInstance().GetItems()){
            
            GameObject cellItem = new GameObject();
            RectTransform rect = cellItem.AddComponent<RectTransform>();
            Image img = cellItem.AddComponent<Image>();
            img.preserveAspect = true;

            rect.pivot = new Vector2(0, 1);
            rect.anchorMin = new Vector2(0, 1);
            rect.anchorMax = new Vector2(0, 1);

            cellItem.transform.SetParent(gameObject.transform, true);

            cellItem.transform.localScale = new Vector3(1, 1, 1);
            cellItem.transform.position = new Vector3(0, 0, 0);

            cellItem.AddComponent<DragDrop>().itemSelected = itemSelected;
            cellItem.AddComponent<EventTrigger>();

            rect.offsetMin = new Vector2(ix * cellWidth, -((iy+1) * cellHeight));
            rect.offsetMax = new Vector2((ix+1) * cellWidth, -iy * cellHeight);

            img.sprite = item.image;


            ix ++;
            if(ix > cols){
                ix = 0;
                iy++;
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBack(){
        SceneManager.UnloadSceneAsync(2);
        

    }
}
