using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class InspectionManager : MonoBehaviour
{
    //Prefabs
    public GameObject personagemPrefab;
    public GameObject pistaPrefab;
    public GameObject exitPrefab;

    public string cenario_pista;
    [TextArea]
    public string cenario_desc;

    //Grupos
    public GameObject personagens_folder;
    public GameObject pistas_folder;
    public GameObject exits_folder;
    public TextureManager textureManager;
    public SpeechManager speechManager;

    public FadeEffect fadeEffect;
    ScenarioData scenarioData;
    [HideInInspector]
    public bool mouseOnSeta;
    // Start is called before the first frame update
    void Start()
    {
        string scenarioName = GlobalProfile.GetCurrentSceneName();

        GlobalProfile.getInstance().LoadGame(textureManager);
        GlobalProfile.getInstance().addItem(new InventoryItem(cenario_pista, scenarioName, textureManager.GetSpritePista(cenario_pista), cenario_desc));
        GlobalProfile.getInstance().SaveGame();
        //Carrega dados do cenário
        if(SaveGameSystem.DoesSaveGameExist("slot"+GlobalProfile.Slot+"_" + scenarioName)){
            scenarioData = (ScenarioData)SaveGameSystem.LoadGame("slot"+GlobalProfile.Slot+"_" + scenarioName);

            if(scenarioData != null){
                //Limpando os personagens e instanciando de novo
                foreach (Transform child in personagens_folder.transform) {
                    GameObject.Destroy(child.gameObject);
                }

                for(int i = 0; i < scenarioData.characters.Count; i ++){
                    GameObject character = Instantiate(personagemPrefab);
                    character.transform.SetParent(personagens_folder.transform, false);
                    character.GetComponent<SpeechableCharacter>().LoadData(scenarioData.characters[i], speechManager, this);
                }

                //Limpando pistas e instanciando de novo
                foreach (Transform child in pistas_folder.transform) {
                    GameObject.Destroy(child.gameObject);
                }

                for(int i = 0; i < scenarioData.pistas.Count; i ++){
                    GameObject pista = Instantiate(pistaPrefab);
                    pista.transform.SetParent(pistas_folder.transform, false);
                    pista.GetComponent<PistaItem>().LoadData(scenarioData.pistas[i], speechManager, this);
                }

                //Limpando exits e instanciando de novo
                foreach(Transform child in exits_folder.transform){
                    GameObject.Destroy(child.gameObject);
                }

                for(int i = 0; i < scenarioData.exits.Count; i ++){
                    GameObject exit = Instantiate(exitPrefab);
                    exit.transform.SetParent(exits_folder.transform, false);
                    exit.GetComponent<ExitPoint>().LoadData(scenarioData.exits[i], this, speechManager);
                }

            }
        }


        if(GlobalProfile.getInstance().dialogIgnition != null){
            speechManager.OpenText(GlobalProfile.getInstance().dialogIgnition);
        }

        //string output = JsonUtility.ToJson(CreateScenarioData(),true);
        //Debug.Log(output);

    }

    public void PropagateMessage(string message){
        //Propaga pros persoangens
        SpeechableCharacter[] characters = (SpeechableCharacter[]) GameObject.FindObjectsOfType(typeof(SpeechableCharacter));
        foreach(SpeechableCharacter c in characters){
            c.ReceiveMessage(message);
        }

        ExitPoint[] points = (ExitPoint[]) GameObject.FindObjectsOfType(typeof(ExitPoint));
        foreach(ExitPoint e in points){
            e.ReceiveMessage(message);
        }
    }

    public void RefreshAllCharacterDialogData()
    {
        foreach(SpeechableCharacter character in personagens_folder.transform.GetComponentsInChildren<SpeechableCharacter>()) {
            character.RefreshDialogData();
        }
        SaveGame();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame(){
        ScenarioData data = CreateScenarioData();
        bool succ = SaveGameSystem.SaveGame(data, "slot"+GlobalProfile.Slot+"_"+ GlobalProfile.GetCurrentSceneName());
        GlobalProfile.getInstance().SaveGame();
    }

    public void ClickOnInventory(){ 
        SaveGame();
        GlobalProfile.getInstance().lastScenarioBeforeInventory = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(2);
    }

    public void SetaEnter(){
        mouseOnSeta = true;
    }

    public void SetaExit(){
        mouseOnSeta = false;
    }

    public ScenarioData CreateScenarioData(){
        ScenarioData data = new ScenarioData();
        
        //Itera por todos os personagens
        foreach(Transform obj in personagens_folder.transform){
            SpeechableCharacter character = obj.GetComponent<SpeechableCharacter>();
            data.characters.Add(character.data);
        }
        //Itera por todas as pistas
        foreach(Transform obj in pistas_folder.transform){
            PistaItem pista = obj.GetComponent<PistaItem>();
            data.pistas.Add(pista.data);
        }
        //Itera por todos os exits
        foreach(Transform obj in exits_folder.transform){
           data.exits.Add(obj.GetComponent<ExitPoint>().GetData());
        }

        return data;
    }
}
