using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistaItem : MonoBehaviour
{
    public LupaButton lupa;
    public SpeechManager speech;
    SpriteRenderer spriteRenderer;

    public ScriptablePista scriptable;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
        scriptable.position = transform.position;
        scriptable.scale = transform.localScale;
        scriptable.rotation = transform.rotation;
    }

    // Update is called once per frame
    void OnMouseDown(){
        if(lupa.pressed){
            //Adicionar ao inventário
            //Abrir um texto
            GlobalProfile.getInstance().addItem(new InventoryItem(scriptable.itemId, scriptable.displayName, spriteRenderer.sprite));
            //Destruir item
            speech.OpenText(scriptable.dialogo.texts);
            Destroy(gameObject);
        }
    }

    public void LoadData(ScriptablePista data, LupaButton lupa, SpeechManager speech){
        GetComponent<SpriteRenderer>().sprite = data.image;
        scriptable = data;
        transform.position = data.position;
        transform.localScale = data.scale;
        transform.rotation = data.rotation;
        this.lupa = lupa;
        this.speech = speech;
    }
}
