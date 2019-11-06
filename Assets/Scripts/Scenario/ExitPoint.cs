using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ExitPoint : MonoBehaviour
{
    [HideInInspector]
    public Texture2D tCursor;
    [HideInInspector]
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public InspectionManager manager;
    public SpeechManager speech;
    public Text childText;

    public Texture2D cursorUp, cursorDown, cursorLeft, cursorRight;

    public bool isUI;

    [HideInInspector]
    public ExitData data = new ExitData();

    float alpha = 0;
    bool mouseOver = false;

    public GameObject innerGroup;

    // Start is called before the first frame update
    void Start()
    {  
        RefreshDialogData();
    }

    public void RefreshDialogData()
    {
        if (GlobalProfile.getInstance().HasReceivedMessage(data.unlockMessage)) 
            data.enabled = true;
    }

    public void ReceiveMessage(string message){
        if (message == data.unlockMessage) 
            data.enabled = true;   
    }

    public void SetCursorDirection(int opt){
        data.iCursor = opt;
        switch(opt){
            case 0:
                tCursor = cursorLeft;
                break;
            case 1:
                tCursor = cursorRight;
                break;
            case 2:
                tCursor = cursorUp;
                break;
            case 3:
                tCursor = cursorDown;
                break;

        }
    }

    public void SetExitPoint(int exitPoint){
        if(data == null) data = new ExitData();
        this.data.exitPoint = exitPoint;
    }

    public int GetExitPoint(){
        return GetData().exitPoint;
    }

    // Update is called once per frame
    void Update()
    {   
        if(data == null) data = new ExitData();
        childText.text = data.title;

        if(mouseOver)
            alpha += (1 - alpha) / 5f;
        else
            alpha += (0 - alpha) / 5f;


        if(innerGroup != null){
            Vector3 localPos = innerGroup.transform.localPosition;
            localPos.y = alpha * 5;
            innerGroup.transform.localPosition = localPos;

            innerGroup.GetComponentInChildren<Image>().sprite = manager.textureManager.GetSpritePista(data.icon_frame);

            #if UNITY_EDITOR 
                if (!Application.isPlaying) return; //or whatever script needs to be run when in edition mode.
            #endif

            innerGroup.GetComponentInChildren<Text>().color = new Color(1, 1, 1, alpha);
            if(data.enabled)
                innerGroup.GetComponentInChildren<Image>().color = new Color(1, 1, 1, alpha);
            else
                innerGroup.GetComponentInChildren<Image>().color = new Color(0, 0, 0, alpha);
        }
    }
    public void Exit(){
        //TODO: Salva cenário
        if(data.enabled){
            ScenarioData data = manager.CreateScenarioData();
            bool succ = SaveGameSystem.SaveGame(data, "slot"+GlobalProfile.Slot+"_"+ GlobalProfile.GetCurrentSceneName());
            GlobalProfile.getInstance().SaveGame();
            Cursor.SetCursor(null, hotSpot, cursorMode);
            manager.fadeEffect.ExitScene(this.data.exitPoint);
        }
    }

    public void LoadData(ExitData data, InspectionManager manager, SpeechManager speech){
        transform.position = data.position;
        transform.localScale = data.scale;
        transform.rotation = data.rotation;
        this.data = data;
        SetCursorDirection(data.iCursor);
        childText.text = data.title;
        this.manager = manager;
        this.speech = speech;
    }

    public ExitData GetData(){
        if(data == null) data = new ExitData();
        data.position = transform.position;
        data.scale = transform.localScale;
        data.rotation = transform.rotation;
        
        return data;
    }

    public void SetEnabled(bool enabled){
        GetData().enabled = enabled;
    }

    public void CursorEnter() {
        if(!speech.isActiveAndEnabled){
            Cursor.SetCursor(tCursor, hotSpot, cursorMode);
            if(!isUI){
                //Faz efeitinho de aparecer
                mouseOver = true;
            }
        }
    }
    public void CursorExit() {
        if(!speech.isActiveAndEnabled){
            Cursor.SetCursor(null, hotSpot, cursorMode);
            if(!isUI){
                //Faz efeitinho de sumir
                mouseOver = false;
            }
        }
    }
}
