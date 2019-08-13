using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPoint : MonoBehaviour
{
    public InspectionManager manager;
    public enum Scenario : int{
        Hall = 2,
        Biblioteca = 1
    }

    public Scenario exitPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit(){
        //TODO: Salva cenário
        bool succ = SaveGameSystem.SaveGame(manager.CreateScenarioData(), "slot0_" + manager.scenarioName);
        Debug.Log("Salvando arquivo " + "slot0_" + manager.scenarioName + ", sucesso? " + succ);
        SceneManager.LoadScene((int)exitPoint);
    }

    public void LoadData(ExitData data, InspectionManager manager){
        transform.position = data.position;
        transform.localScale = data.scale;
        transform.rotation = data.rotation;
        exitPoint = (Scenario) data.exitPoint;
        this.manager = manager;
    }

    public ExitData GetData(){
        ExitData data = new ExitData();
        data.exitPoint = (int)exitPoint;
        data.position = transform.position;
        data.scale = transform.localScale;
        data.rotation = transform.rotation;
        return data;
    }
}
