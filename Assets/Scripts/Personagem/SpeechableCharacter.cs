using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpeechableCharacter : MonoBehaviour
{
    public SpeechManager speechCanvas;
    public InspectionManager manager;
    public LupaButton lupa;

    public ScriptableCharacter personagem_data;
    //public Sprite defaultImage;
    //public Sprite headBob;

    //Verifica se ele falou com esse personagem pelo menos 1 vez pra gerar a pista do personagem
    bool hasTalked = false;
   
    //public List<Dialogo> dialogos = new List<Dialogo>();

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
        personagem_data.position = transform.position;
        personagem_data.scale = transform.localScale;
        personagem_data.rotation = transform.rotation;
    }

    // Update is called once per frame
    void OnMouseOver(){
        if(Input.GetMouseButtonDown(0) && !manager.mouseOnSeta){ //perguntar se o mouse não tá em cima da seta
            if(personagem_data.dialogos.Count > 0)
                selectCharacter();
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
        return (from item in personagem_data.dialogos
            where item.enabled select item).ToList();
    }

    public void LoadData(ScriptableCharacter script, SpeechManager canvas, InspectionManager manager, LupaButton lupa){
        GetComponent<SpriteRenderer>().sprite = script.defaultImage;
        personagem_data = script;
        transform.position = script.position;
        transform.localScale = script.scale;
        transform.rotation = script.rotation;
        this.speechCanvas = canvas;
        this.manager = manager;
        this.lupa = lupa;
    }
   
}