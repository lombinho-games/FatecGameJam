using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistaItem : MonoBehaviour
{
    public LupaButton lupa;
    public SpeechManager speech;

    public string displayName;
    public string itemId;

    public TextData[] texts;

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
            speech.OpenText(texts);
            

            Destroy(gameObject);
        }

    }
}
