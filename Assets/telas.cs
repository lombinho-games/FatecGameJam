using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class telas : MonoBehaviour
{
    public ScenarioCamera scenarioCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Passar(){
        scenarioCamera.cena += 1;
        Camera.main.transform.position = new Vector3(
            0, Camera.main.transform.position.y,
            Camera.main.transform.position.z
        );
        Debug.Log(scenarioCamera.cena);
    }
    public void Voltar(){
        scenarioCamera.cena -=1;
        Camera.main.transform.position = new Vector3(
            0, Camera.main.transform.position.y,
            Camera.main.transform.position.z
        );
        Debug.Log(scenarioCamera.cena);
    }
    
}
