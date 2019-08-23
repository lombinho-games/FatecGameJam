using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemMenu : MonoBehaviour
{

    [HideInInspector]
    public PistaFrame selected;
    [HideInInspector]
    public GameObject pistaSlot;

    public GameObject mouse;
    public GameObject lineGroup;
    public Quadro quadro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        gameObject.SetActive(false);


        GameObject lineConection = new GameObject("Line");
        ItemConnection connection = lineConection.AddComponent<ItemConnection>();
        connection.width = 5;
        connection.objectA = selected.outerPin;
        connection.objectB = mouse;
        connection.isOnMouse = true;

        lineConection.transform.SetParent(lineGroup.transform, false);

        quadro.creatingConnection = connection;

    }

    public void Remove() {

        //Destroi a conexão se existe alguma

        Destroy(selected.outerPin.gameObject);
        Destroy(selected.gameObject);
        pistaSlot.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        gameObject.SetActive(false);

    }
}
