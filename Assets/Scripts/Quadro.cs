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

    public TextureManager textureManager;

    public bool mouseOver;

    public Inventory inventory;

    public GameObject connectionPrefab;
    public GameObject connectionPanel;
    public GameObject lineGroup;
    // Start is called before the first frame update
    void Start()
    {
        if(SaveGameSystem.DoesSaveGameExist("slot0_quadro")){
            LoadQuadroData(SaveGameSystem.LoadGame("slot0_quadro") as QuadroData);
        }
    }

    void OnMouseEnter(){
        mouseOver = true;
    }

    void OnMouseExit(){
        mouseOver = false;
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

    public void SaveQuadro(){
        QuadroData data = CreateQuadroData();
        SaveGameSystem.SaveGame(data, "slot0_quadro");
    }

    public QuadroData CreateQuadroData(){

        QuadroData data = new QuadroData();
        PistaFrame[] pistas = content.transform.GetComponentsInChildren<PistaFrame>();

        foreach(PistaFrame pista in pistas){
            QuadroData.Item i = new QuadroData.Item();
            i.name = pista.item.displayName;
            i.id = pista.item.itemID;
            i.description = pista.item.description;
            i.position = pista.transform.position;
            data.items.Add(i);
        }

        ItemConnection[] connections = content.transform.Find("LinhasGroup").transform.GetComponentsInChildren<ItemConnection>();

        foreach(ItemConnection conn in connections){

            QuadroData.Connection connect = new QuadroData.Connection();
            connect.itemA = -1;
            connect.itemB = -1;
            connect.connectionName = conn.connector.ToString();

            PistaFrame pistaA = conn.objectA.GetComponent<PistaPin>().pista.transform.parent.GetComponent<PistaFrame>();
            PistaFrame pistaB = conn.objectB.GetComponent<PistaPin>().pista.transform.parent.GetComponent<PistaFrame>();

            for(int i = 0; i < pistas.Length; i ++){
                if(pistas[i] == pistaA){
                    connect.itemA = i;
                }
                if(pistas[i] == pistaB){
                    connect.itemB = i;
                }
                if(connect.itemA != -1 && connect.itemB != -1) break;
            }
            data.connections.Add(connect);
        }

        return data;
    }

    public void LoadQuadroData(QuadroData data){

        List<PistaFrame> frames = new List<PistaFrame>();

        foreach(QuadroData.Item item in data.items){
            InventoryItem inv = GlobalProfile.getInstance().GetItems(textureManager).Find(new System.Predicate<InventoryItem>( (InventoryItem i) => {
                return i.itemID == item.id;
            } ));
            frames.Add(inventory.InsertPistaFrame(item.position, inv));
        }

        foreach(QuadroData.Connection conn in data.connections){

            GameObject lineConection = Instantiate(connectionPrefab);
            ItemConnection connection = lineConection.GetComponent<ItemConnection>();
            connection.connectorSelector = connectionPanel;
            connection.objectA = frames[conn.itemA].outerPin;
            connection.objectB = frames[conn.itemB].outerPin;
            connection.isOnMouse = false;
            connection.menu = menu;
            lineConection.transform.SetParent(lineGroup.transform, false);

        }

    }
    
}
