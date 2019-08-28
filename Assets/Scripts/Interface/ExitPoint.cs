﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPoint : MonoBehaviour
{
    public Texture2D tCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public InspectionManager manager;
    public SpeechManager speech;
    public enum Scenario : int{
        Hall = 2,
        Biblioteca = 1
    }

    public Scenario exitPoint;

    // Start is called before the first frame update
    void Start()
    {  
        foreach (Transform t in transform){
          t.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit(){
        //TODO: Salva cenário
        ScenarioData data = manager.CreateScenarioData();
        bool succ = SaveGameSystem.SaveGame(data, "slot0_" + manager.scenarioName);
        GlobalProfile.getInstance().SaveGame();
        Cursor.SetCursor(null, hotSpot, cursorMode);
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
    public void CursorEnter() {
        if(!speech.isActiveAndEnabled){
            Cursor.SetCursor(tCursor, hotSpot, cursorMode);
            foreach (Transform t in transform){
                t.gameObject.SetActive(true);
            }
        }
    }
    public void CursorExit() {
        if(!speech.isActiveAndEnabled){
            Cursor.SetCursor(null, hotSpot, cursorMode);
            foreach (Transform t in transform){
                t.gameObject.SetActive(false);
            }
        }
    }
}
