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
    [HideInInspector]
    public ItemConnection connection;
    public GameObject connectionPanel;
    public GameObject descriptionPanel;

    public GameObject mouse;
    public GameObject lineGroup;
    public Quadro quadro;
    public GameObject connectionPrefab;

    //Botões
    public GameObject connectButton;
    public GameObject removeButton;
    public GameObject descButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenMenu(PistaFrame pista, GameObject pistaSlot, ItemConnection connection, bool connectButton, bool descButton, bool removeButton)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        transform.position = position;
        gameObject.SetActive(true);
        selected = pista;
        this.pistaSlot = pistaSlot;
        this.connection = connection;

        this.connectButton.SetActive(connectButton);
        this.removeButton.SetActive(removeButton);
        this.descButton.SetActive(descButton);
    }

    public void Connect()
    {
        gameObject.SetActive(false);
        GameObject lineConection = Instantiate(connectionPrefab);
        ItemConnection connection = lineConection.GetComponent<ItemConnection>();
        connection.connectorSelector = connectionPanel;
        connection.objectA = selected.outerPin;
        connection.objectB = mouse;
        connection.isOnMouse = true;
        connection.menu = this;
        lineConection.transform.SetParent(lineGroup.transform, false);
        quadro.creatingConnection = connection;

    }

    public void Remove() {

        //Destroi a conexão se existe alguma

        if (selected != null) {

            for(int i = 0; i < lineGroup.transform.childCount; i++) {
                ItemConnection conn = lineGroup.transform.GetChild(i).GetComponent<ItemConnection>();
                if(conn.objectA == selected.outerPin || conn.objectB == selected.outerPin) {
                    Destroy(conn.gameObject);
                }
            }

            Destroy(selected.outerPin.gameObject);
            Destroy(selected.gameObject);
            pistaSlot.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        if(connection != null) {
            Destroy(connection.gameObject);
        }

        gameObject.SetActive(false);

    }

    public void CloseConnectionSelector(){
        connectionPanel.gameObject.SetActive(false);
    }

    public void Description(){

        if(selected != null){

            descriptionPanel.GetComponentInChildren<Text>().text = selected.item.description;
            descriptionPanel.SetActive(true);

            return;
        }

    }
}
