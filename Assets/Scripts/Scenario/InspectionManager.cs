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

    public CharacterList characters;
    public PistaList pistas;

    public string scenarioName;
    public LupaButton lupa;
    public SpeechManager speechManager;
    ScenarioData scenarioData;
    [HideInInspector]
    public bool mouseOnSeta;
    // Start is called before the first frame update
    void Start()
    {
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

                    ScriptableCharacter script = characters.FindValue(scenarioData.characters[i]);
                    character.GetComponent<SpeechableCharacter>().LoadData(script, speechManager, this, lupa);
                }

                //Limpando pistas e instanciando de novo
                foreach (Transform child in pistas_folder.transform) {
                    GameObject.Destroy(child.gameObject);
                }

                for(int i = 0; i < scenarioData.pistas.Count; i ++){
                    GameObject pista = Instantiate(pistaPrefab);
                    pista.transform.SetParent(pistas_folder.transform, false);

                    ScriptablePista script = pistas.FindValue(scenarioData.pistas[i]);
                    pista.GetComponent<PistaItem>().LoadData(script, lupa, speechManager);
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickOnInventory(){
        if(!lupa.pressed){
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        }
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
             //procura esse cara na minha lista, e salva só o nome
            string key = characters.FindKey(obj.GetComponent<SpeechableCharacter>().personagem_data);
            if(key != null){
                data.characters.Add(key);
            }
        }
        //Itera por todas as pistas
        foreach(Transform obj in pistas_folder.transform){
            string key = pistas.FindKey(obj.GetComponent<PistaItem>().scriptable);
            if(key != null){
                data.pistas.Add(key);
            }
        }
        //Itera por todos os exits
        foreach(Transform obj in exits_folder.transform){
           // data.exits.Add(obj.GetComponent<ExitPoint>().GetData());
        }

        return data;
    }
}
