using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistaItem : MonoBehaviour
{
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
        if(lupa.pressed){
            //Adicionar ao inventário
            //Abrir um texto
            GlobalProfile.getInstance().addItem(new InventoryItem(data.itemId, data.displayName, spriteRenderer.sprite));
            //Destruir item
            speech.OpenText(data.dialogo.texts);
            Destroy(gameObject);
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
}
