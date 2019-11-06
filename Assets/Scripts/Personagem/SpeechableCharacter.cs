using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpeechableCharacter : MonoBehaviour
{
    public CursorMode CursorMode = CursorMode.Auto;
    public Texture2D ReadTexture2D;
    public Texture2D NReadTexture2D;
    public Vector2 hotSpot = Vector2.zero;
    public SpeechManager speechCanvas;
    public InspectionManager manager;
    SpriteRenderer spriteRenderer;

    //Data
    public CharacterData data;

    public GameObject exclamation;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        RefreshDialogData();
       
    }

    void Update(){
        data.position = transform.position;
        data.scale = transform.localScale;
        data.rotation = transform.rotation;

        bool exl = false;
        foreach(Dialogo dig in AvailableDialogs()){
            if(dig.enabled && !dig.read){
                exl = true;
                break;
            }
        }
        exclamation.SetActive(exl);
    }

    // Update is called once per frame
    void OnMouseOver(){
        if(Input.GetMouseButtonDown(0) && !manager.mouseOnSeta){ //perguntar se o mouse não tá em cima da seta
            if(data.dialogos.Count > 0){
                selectCharacter();
            }
        }
    }

    public void selectCharacter(){
        if(!speechCanvas.gameObject.activeInHierarchy){
            //Setar os valores do canvas
            SpeechManager sm = speechCanvas.GetComponent<SpeechManager>();
            sm.OpenCharacterDialog(this);
        }
    }

    public List<Dialogo> AvailableDialogs(){
        return (from item in data.dialogos
            where item.enabled select item).ToList();
    }

    public void LoadData(CharacterData data, SpeechManager canvas, InspectionManager manager){
        GetComponent<SpriteRenderer>().sprite = manager.textureManager.GetSpritePose(data.defaultImage);
        this.data = data;
        transform.position = data.position;
        transform.localScale = data.scale;
        transform.rotation = data.rotation;
        this.speechCanvas = canvas;
        this.manager = manager;
    }

    public void ReceiveMessage(string message){
        foreach (Dialogo d in data.dialogos) {
            if (message == d.unlock_message) {
                d.enabled = true;
            }
        }
    }

    public void RefreshDialogData()
    {
        foreach (Dialogo d in data.dialogos) {
            if (GlobalProfile.getInstance().HasReceivedMessage(d.unlock_message)) {
                d.enabled = true;
            }
        }
    }

    public void OnMouseEnter() {

        if (Input.GetMouseButtonDown(0)) {
            RefreshDialogData();
        }

        if(!speechCanvas.isActiveAndEnabled){
            bool cRead = true;
            if(data.dialogos.Count > 0){
                foreach(Dialogo d in data.dialogos){

                    if(d.enabled && !d.read){
                        cRead = false;
                    }
                }
            }
            if(!cRead){
                Cursor.SetCursor(ReadTexture2D,hotSpot,CursorMode); // Cursor de novo Texto disponivel      
            }
            else{
                Cursor.SetCursor(NReadTexture2D,hotSpot,CursorMode); // Cursor de Texto base
            }
        }
    }
    public void OnMouseExit() {
        if(!speechCanvas.isActiveAndEnabled)
            Cursor.SetCursor(null,hotSpot,CursorMode);
    }   
}