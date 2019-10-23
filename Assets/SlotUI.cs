using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class SlotUI : MonoBehaviour
{

    public Text dataInicioText;
    public Text cenarioText;
    public Text tempoDeJogoText;
    public Image detetiveImage;
    public Image cenarioImage;
    

    [Header("Propriedades")]
    public int id;
    public long dataInicio;
    public int cenario;
    public long tempoDeJogo;
    public Sprite detetive;
    public Sprite cenarioSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DateTime dataInicioDate = new DateTime(dataInicio);
        dataInicioText.text = dataInicioDate.Day + "/" + dataInicioDate.Month + "/" + dataInicioDate.Year;

        Debug.Log("Nome da cena: " + GetSceneNameByPath(SceneUtility.GetScenePathByBuildIndex(cenario)));
        cenarioText.text = GetSceneNameByPath(SceneUtility.GetScenePathByBuildIndex(cenario));

        DateTime tempoJogoDate = new DateTime(tempoDeJogo);
        tempoDeJogoText.text = tempoJogoDate.Hour + ":" + tempoJogoDate.Minute + ":" + tempoJogoDate.Second;

        if(detetive != null)
            detetiveImage.sprite = detetive;
        if(cenarioSprite != null)
        cenarioImage.sprite = cenarioSprite;
    }

    public string GetSceneNameByPath(string sceneName){
        string[] ped = sceneName.Split(new char[] {'/'});
        return ped[ped.Length-1].Split(new char[] {'.'})[0];
    }

    public void LoadGame(){
        GlobalProfile.Slot = id;
        SceneManager.LoadScene(cenario);
    }

    public void DeleteSave(){
        // O ideal é que apareça um dialoig de confirmação

        GlobalProfile.gameSlots.Slots.Remove(id);
        Destroy(gameObject);

        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++){
            string name = GetSceneNameByPath(SceneUtility.GetScenePathByBuildIndex(i));
            if(SaveGameSystem.DoesSaveGameExist("slot" + id + "_" + name)){
                SaveGameSystem.DeleteSaveGame("slot" + id + "_" + name);
            }
        }
        if(SaveGameSystem.DoesSaveGameExist("slot" + id + "_inventory")){
            SaveGameSystem.DeleteSaveGame("slot" + id + "_inventory");
        }
        if(SaveGameSystem.DoesSaveGameExist("slot" + id + "_messages")){
            SaveGameSystem.DeleteSaveGame("slot" + id + "_messages");
        }

    }
}
