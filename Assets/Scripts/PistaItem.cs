using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistaItem : MonoBehaviour
{
    public LupaButton lupa;
    public SpeechManager speech;
    public string displayName;
    public string itemId;
    public Dialogo dialogo;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void OnMouseDown(){
        if(lupa.pressed){
            //Adicionar ao inventário
            //Abrir um texto
            GlobalProfile.getInstance().addItem(new InventoryItem(itemId, displayName, spriteRenderer.sprite));
            //Destruir item
            speech.OpenText(dialogo.texts);
            Destroy(gameObject);
        }
    }

    public void LoadData(PistaData data, LupaButton lupa, SpeechManager speech){
        dialogo = data.dialogo;
        displayName = data.displayName;
        itemId = data.itemId;
        transform.position = data.position;
        transform.localScale = data.scale;
        transform.rotation = data.rotation;
        this.lupa = lupa;
        this.speech = speech;
    }

    public PistaData GetData(){
        PistaData data = new PistaData();
        data.dialogo = dialogo;
        data.displayName = displayName;
        data.itemId = itemId;
        data.position = transform.position;
        data.scale = transform.localScale;
        data.rotation = transform.rotation;
        return data;
    }
}
