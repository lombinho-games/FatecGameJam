using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistaItem : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public LupaButton lupa;
    public SpeechManager speech;
    public InspectionManager manager;
    SpriteRenderer spriteRenderer;

    public PistaData data;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
        data.position = transform.position;
        data.scale = transform.localScale;
        data.rotation = transform.rotation;
    }

    // Update is called once per frame
    void OnMouseDown(){
<<<<<<< HEAD
        if(lupa.pressed){
            GlobalProfile.getInstance().addItem(new InventoryItem(data.itemId, data.displayName, spriteRenderer.sprite));
=======
        if(!speech.isActiveAndEnabled){
            //Adicionar ao inventário
            //Abrir um texto
            GlobalProfile.getInstance().addItem(new InventoryItem(data.itemId, data.displayName, spriteRenderer.sprite, data.itemDescription));
            //Destruir item
>>>>>>> ccabfbc0dfcb964e3a34a8441e237b22df95bebf
            speech.OpenText(data.dialogo.texts);
            Cursor.SetCursor(null, new Vector2(), CursorMode.Auto);
            Destroy(gameObject);
            manager.RefreshAllCharacterDialogs();
        }
    }

    public void LoadData(PistaData data, LupaButton lupa, SpeechManager speech, InspectionManager manager){
        GetComponent<SpriteRenderer>().sprite = manager.textureManager.GetSpritePista(data.image);
        this.manager = manager;
        this.data = data;
        transform.position = data.position;
        transform.localScale = data.scale;
        transform.rotation = data.rotation;
        this.lupa = lupa;
        this.speech = speech;
    }
    private void OnMouseOver() {
        if(!speech.isActiveAndEnabled)
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);    
    }
    private void OnMouseExit() {
        if(!speech.isActiveAndEnabled)
            Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}
