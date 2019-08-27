using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InspectionManager : MonoBehaviour
{

    //Prefabs
    public GameObject personagemPrefab;
    public GameObject pistaPrefab;

    //Grupos
    public GameObject personagens_folder;
    public GameObject pistas_folder;
    public GameObject exits_folder;
    public TextureManager textureManager;

    public string scenarioName;
    public SpeechManager speechManager;
    ScenarioData scenarioData;
    [HideInInspector]
    public bool mouseOnSeta;
    // Start is called before the first frame update
    void Start()
    {
        GlobalProfile.getInstance().LoadGame(textureManager);
        //Carrega dados do cenário
        if(SaveGameSystem.DoesSaveGameExist("slot0_" + scenarioName)){
            scenarioData = (ScenarioData)SaveGameSystem.LoadGame("slot0_" + scenarioName);

            if(scenarioData == null){
                Debug.Log("Arquivo slot0_" + scenarioName + " com problemas ao ser carregado");
            }
            else{
                Debug.Log("Carreguei os dados de cenário " + scenarioName);
                Debug.Log("Achei " + scenarioData.characters.Count + " personagens e " + scenarioData.pistas.Count + " pistas");
                
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

                //TODO: limpar as pastas e instanciar a galera;
            }
        }
        else{
            Debug.Log("Arquivo slot0_" + scenarioName + " não foi encontrado");
        }

        //string output = JsonUtility.ToJson(CreateScenarioData(),true);
        //Debug.Log(output);

    }

    public void RefreshAllCharacterDialogData()
    {
        foreach(SpeechableCharacter character in personagens_folder.transform.GetComponentsInChildren<SpeechableCharacter>()) {
            character.RefreshDialogData();
        }
        GlobalProfile.getInstance().SaveGame();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickOnInventory(){ 
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
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
           // data.exits.Add(obj.GetComponent<ExitPoint>().GetData());
        }

        return data;
    }
}
