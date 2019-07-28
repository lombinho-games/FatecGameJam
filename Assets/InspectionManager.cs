using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InspectionManager : MonoBehaviour
{

    public TextData[] initialTexts;
    public SpeechManager speech;
    public LupaButton lupa;
    // Start is called before the first frame update
    void Start()
    {
        speech.OpenText(initialTexts);
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
}
