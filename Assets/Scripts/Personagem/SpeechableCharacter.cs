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
    public LupaButton lupa;
    SpriteRenderer spriteRenderer;

    //Data
    public CharacterData data;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        RefreshDialogData();
    }

    public void RefreshDialogData()
    {
        foreach (Dialogo d in data.dialogos) {
            if (GlobalProfile.getInstance().HasReceivedMessage(d.unlock_message)) {
                Debug.Log("Encontrei a mensagem " + d.unlock_message + ", destravando dialogo " + d.message);
                d.enabled = true;
            }
        }
    }

    void Update(){
        data.position = transform.position;
        data.scale = transform.localScale;
        data.rotation = transform.rotation;
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
        if(!speechCanvas.gameObject.activeInHierarchy && !lupa.pressed){
            //Setar os valores do canvas
            SpeechManager sm = speechCanvas.GetComponent<SpeechManager>();
            sm.OpenCharacterDialog(this);
        }
    }

    public List<Dialogo> AvailableDialogs(){
        return (from item in data.dialogos
            where item.enabled select item).ToList();
    }

    public void LoadData(CharacterData data, SpeechManager canvas, InspectionManager manager, LupaButton lupa){
        GetComponent<SpriteRenderer>().sprite = manager.textureManager.GetSpritePose(data.defaultImage);
        this.data = data;
        transform.position = data.position;
        transform.localScale = data.scale;
        transform.rotation = data.rotation;
        this.speechCanvas = canvas;
        this.manager = manager;
        this.lupa = lupa;
    }

    public void OnMouseEnter() {

        Texture2D mouse = NReadTexture2D;

         foreach (Dialogo d in data.dialogos){
             if(d.enabled && !d.read){
                mouse = ReadTexture2D;
                break;
             }
         }
        Cursor.SetCursor(mouse, hotSpot, CursorMode);
    }
    public void OnMouseExit() {
        Cursor.SetCursor(null,hotSpot,CursorMode);
    }   
}