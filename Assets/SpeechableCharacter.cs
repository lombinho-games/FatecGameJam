using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechableCharacter : MonoBehaviour
{
    public Canvas speechCanvas;
    public InspectionManager manager;
    public LupaButton lupa;

    [TextArea(3,10)]
    public string[] texts;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void OnMouseOver(){
        if(Input.GetMouseButtonDown(0)){
            selectCharacter();
        }
    }

    public void selectCharacter(){
        if(!speechCanvas.gameObject.activeInHierarchy && !lupa.pressed){
            //Setar os valores do canvas
            SpeechManager sm = speechCanvas.GetComponent<SpeechManager>();
            sm.OpenText(spriteRenderer.sprite, texts);
        }
    }
}