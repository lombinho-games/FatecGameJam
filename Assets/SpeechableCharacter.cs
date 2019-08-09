using System.Collections.Generic;
using UnityEngine;

public class SpeechableCharacter : MonoBehaviour
{
    public Canvas speechCanvas;
    public InspectionManager manager;
    public LupaButton lupa;
    public GameObject personagens;
    public GameObject gui;

    public Sprite headBob;

    //Verifica se ele falou com esse personagem pelo menos 1 vez pra gerar a pista do personagem
    bool hasTalked = false;
   
    public List<Dialogo> dialogos = new List<Dialogo>();

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void OnMouseOver(){
        if(Input.GetMouseButtonDown(0) && !manager.mouseOnSeta){ //perguntar se o mouse não tá em cima da seta
            selectCharacter();
        }
    }

    public void selectCharacter(){
        
        /*
        if(!speechCanvas.gameObject.activeInHierarchy && !lupa.pressed){
            //Setar os valores do canvas
            SpeechManager sm = speechCanvas.GetComponent<SpeechManager>();
            sm.OpenText(texts);
            personagens.SetActive(false);
            gui.SetActive(false);

            if(!hasTalked){
                hasTalked = true;
                GlobalProfile.getInstance().addItem(new InventoryItem(gameObject.name, gameObject.name, headBob));
            }
        }
        */
    }
}