using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpeechableCharacter : MonoBehaviour
{
    public Canvas speechCanvas;
    public InspectionManager manager;
    public LupaButton lupa;
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
            if(dialogos.Count > 0)
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
        return (from item in dialogos
            where item.enabled select item).ToList();
    }

    public void LoadData(CharacterData data, Canvas canvas, InspectionManager manager, LupaButton lupa){
        dialogos = data.dialogos;
        defaultImage = data.defaultImage;
        headBob = data.headBob;
        transform.position = data.position;
        transform.localScale = data.scale;
        transform.rotation = data.rotation;
        this.speechCanvas = canvas;
        this.manager = manager;
        this.lupa = lupa;
    }

    public CharacterData GetData(){
        CharacterData data = new CharacterData();
        data.dialogos = dialogos;
        data.defaultImage = defaultImage;
        data.headBob = headBob;
        data.position = transform.position;
        data.scale = transform.localScale;
        data.rotation = transform.rotation;
        return data;
    }
}