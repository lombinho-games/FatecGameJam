using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpeechableCharacter : MonoBehaviour
{
    public Canvas speechCanvas;
    public InspectionManager manager;
    public LupaButton lupa;
    public GameObject personagens;
    public GameObject gui;
    public Sprite defaultImage;

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
        if(!speechCanvas.gameObject.activeInHierarchy && !lupa.pressed){
            //Setar os valores do canvas
            SpeechManager sm = speechCanvas.GetComponent<SpeechManager>();
            sm.OpenCharacterDialog(this);
            personagens.SetActive(false);
            gui.SetActive(false);
        }
    }

    public List<Dialogo> AvailableDialogs(){
        return (from item in dialogos
            where item.enabled select item).ToList();
    }
}