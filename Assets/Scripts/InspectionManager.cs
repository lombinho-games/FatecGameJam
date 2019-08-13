using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InspectionManager : MonoBehaviour
{

    public GameObject personagens;
    public GameObject pistas;
    public GameObject exits;

    public string scenarioName;
    public LupaButton lupa;
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
                Debug.Log("Achei " + scenarioData.characters.Count + " personagens, " + scenarioData.exits.Count + " saídas e " + scenarioData.pistas.Count + " pistas");
                //TODO: limpar as pastas e instanciar a galera;
            }
        }
        else{
            Debug.Log("Arquivo slot0_" + scenarioName + " não foi encontrado");
        }

        string output = JsonUtility.ToJson(CreateScenarioData(),true);
        Debug.Log(output);

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
        foreach(Transform obj in personagens.transform){
            data.characters.Add(obj.GetComponent<SpeechableCharacter>().GetData());
        }
        //Itera por todas as pistas
        foreach(Transform obj in pistas.transform){
            data.pistas.Add(obj.GetComponent<PistaItem>().GetData());
        }
        //Itera por todos os exits
        foreach(Transform obj in exits.transform){
            data.exits.Add(obj.GetComponent<ExitPoint>().GetData());
        }

        return data;
    }
}
