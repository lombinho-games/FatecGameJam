using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;
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

    public GameObject labelCheck;
    public bool checkSelection = false;

    public SolutionScriptableObject solution;
    public FadeEffect fadeEffect;



    // Start is called before the first frame update
    void Start()
    {
        if(SaveGameSystem.DoesSaveGameExist("slot"+GlobalProfile.Slot+"_quadro")){
            LoadQuadroData(SaveGameSystem.LoadGame("slot"+GlobalProfile.Slot+"_quadro") as QuadroData);
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

        bool canDelete = true;
        foreach(ItemConnection conn in Object.FindObjectsOfType(typeof(ItemConnection))){
            if(conn.status != 0){
                if(pista == conn.GetPistaA() || pista == conn.GetPistaB())
                    canDelete = false;
            }
        }

        menu.OpenMenu(pista, pistaSlot, null, true, true, canDelete);
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

    public void BeginCheck(){
        
        if (menu.gameObject.activeInHierarchy) {
            menu.gameObject.SetActive(false);
        }

        if(!checkSelection){
            checkSelection = true;
            if (creatingConnection != null) {
                Destroy(creatingConnection.gameObject);
                creatingConnection = null;
            }
            labelCheck.SetActive(true);
        }
        else{
            checkSelection = false;
            labelCheck.SetActive(false);

            foreach(PistaFrame pista in Object.FindObjectsOfType(typeof(PistaFrame))){
                pista.selected = false;
            }
        }
    }

    public void ConfirmCheck(){
        checkSelection = false;
        labelCheck.SetActive(false);
        
        List<ItemConnection> conns = Object.FindObjectsOfType(typeof(ItemConnection)).Select( (obj) => {return (ItemConnection) obj ;}).TakeWhile( (conn) => {
            return conn.GetPistaA().selected && conn.GetPistaB().selected;
        }).ToList();

        foreach(PistaFrame pista in Object.FindObjectsOfType(typeof(PistaFrame))){
                pista.selected = false;
        }

        //passa pro GetResponse

        List<SolutionScriptableObject.Solution> solutions = this.solution.GetResponse(conns);
        if(solutions.Count > 0){
            SolutionScriptableObject.Solution solution = solutions[0]; //Da lista de soluções encontradas, procura a que tem mais conexões
            foreach(SolutionScriptableObject.Solution s in solutions){
                if(s.conns.Length > solution.conns.Length){
                    solution = s;
                }
            }

            GlobalProfile.getInstance().dialogIgnition = solution.dialogo.ToList();
            GlobalProfile.getInstance().SendMessage(solution.event_id);

            foreach(ItemConnection conn in conns){
                foreach(SolutionScriptableObject.ConnStruct str in solution.conns){
                    if(CompareConnections(conn, str)){
                        conn.status = solution.correctness;
                    }
                }
            }

            SaveQuadro();
            fadeEffect.ExitScene(GlobalProfile.getInstance().lastScenarioBeforeInventory);
        }
        else{
            foreach(ItemConnection conn in conns){
                PistaFrame pistaA = conn.objectA.GetComponent<PistaPin>().pista.transform.parent.GetComponent<PistaFrame>();
                PistaFrame pistaB = conn.objectB.GetComponent<PistaPin>().pista.transform.parent.GetComponent<PistaFrame>();

                Debug.Log("Pista 1: " + pistaA.item.itemID);
                Debug.Log("Pista 2: " + pistaB.item.itemID);
                Debug.Log("Conexão: " + conn.connector);
            }

        }
    }

    public bool CompareConnections(ItemConnection conn, SolutionScriptableObject.ConnStruct str){
        if(conn.connector == str.connection){
            if(conn.GetPistaA().item.itemID == str.pista1 && conn.GetPistaB().item.itemID == str.pista2)
                return true;
            
            if(conn.GetPistaB().item.itemID == str.pista1 && conn.GetPistaA().item.itemID == str.pista2)
                return true;
        }
        return false;
    }

    public void SaveQuadro(){
        QuadroData data = CreateQuadroData();
        SaveGameSystem.SaveGame(data, "slot"+GlobalProfile.Slot+"_quadro");
        GlobalProfile.getInstance().SaveGame();
    }

    public void BackToGame(){
        SaveQuadro();
        SceneManager.LoadScene(GlobalProfile.getInstance().lastScenarioBeforeInventory);
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
            connect.status = conn.status;
            connect.connectionName = conn.connector;

            for(int i = 0; i < pistas.Length; i ++){
                if(pistas[i] == conn.GetPistaA()){
                    connect.itemA = i;
                }
                if(pistas[i] == conn.GetPistaB()){
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
            connection.status = conn.status;
            connection.connector = conn.connectionName;
            lineConection.transform.SetParent(lineGroup.transform, false);

        }

    }
    
}
