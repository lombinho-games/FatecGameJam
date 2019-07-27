using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InspectionManager : MonoBehaviour
{
    public LupaButton lupa;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickOnInventory(){
        if(!lupa.pressed){
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}
