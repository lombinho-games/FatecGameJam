using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadro : MonoBehaviour
{

    public GameObject content;
    public GameObject menu;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        
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
    
    
}
