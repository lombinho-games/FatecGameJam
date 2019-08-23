using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadro : MonoBehaviour
{

    public GameObject content;
    public GameObject menu;
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
    }

    public void OpenMenu(PistaFrame pista, GameObject pistaSlot)
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        menu.transform.position = position;
        menu.SetActive(true);
        menu.GetComponent<InventoryItemMenu>().selected = pista;
        menu.GetComponent<InventoryItemMenu>().pistaSlot = pistaSlot;
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void QuadroClick()
    {
        if (menu.activeInHierarchy) {
            menu.SetActive(false);
        }

        if (creatingConnection != null) {
            Destroy(creatingConnection.gameObject);
            creatingConnection = null;
        }
    }
    
    
}
