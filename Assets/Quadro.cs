using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Quadro : MonoBehaviour
{

    public GameObject content;
    public InventoryItemMenu menu;
    public Camera mainCamera;
    public GameObject mouse;

    [HideInInspector]
    public ItemConnection creatingConnection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        mouse.transform.position = position;

        if(Input.mouseScrollDelta.y > 0){
            Vector3 vs = content.GetComponent<RectTransform>().localScale;
            vs /= 0.9f;
            vs.z = 1;
            content.GetComponent<RectTransform>().localScale = vs;
        }
        else if(Input.mouseScrollDelta.y < 0){
            Vector3 vs = content.GetComponent<RectTransform>().localScale;
            vs *= 0.9f;
            vs.z = 1;
            content.GetComponent<RectTransform>().localScale = vs;
        }
    }

    public void OpenMenu(PistaFrame pista, GameObject pistaSlot)
    {
        
        menu.OpenMenu(pista, pistaSlot, null, true, true, true);
        
    }

    public void CloseMenu()
    {
        menu.gameObject.SetActive(false);
    }

    public void QuadroClick()
    {
        if (menu.gameObject.activeInHierarchy) {
            menu.gameObject.SetActive(false);
        }

        if (creatingConnection != null) {
            Destroy(creatingConnection.gameObject);
            creatingConnection = null;
        }
    }

    
}
