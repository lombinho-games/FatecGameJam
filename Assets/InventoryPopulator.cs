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
        int cont = 0;
        foreach(InventoryItem item in GlobalProfile.getInstance().GetItems()){
            
            GameObject cellItem = new GameObject();
            RectTransform rect = cellItem.AddComponent<RectTransform>();
            Image img = cellItem.AddComponent<Image>();
            img.preserveAspect = true;

            rect.pivot = new Vector2(.5f, .5f);
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 1);

            cellItem.transform.SetParent(transform.GetChild(cont).transform, true);

            cellItem.transform.localScale = new Vector3(1, 1, 1);
            cellItem.transform.position = new Vector3(0, 0, 0);

            cellItem.AddComponent<DragDrop>().itemSelected = itemSelected;
            cellItem.AddComponent<EventTrigger>();

            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, 0);

            img.sprite = item.image;
            cont++;
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
