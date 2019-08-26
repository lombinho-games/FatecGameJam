using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechManager : MonoBehaviour
{
    // Start is called before the first frame update
    public CursorMode CursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public InspectionManager manager;

    public Texture2D TextureCursor;

    public Texture2D TextureCursorAdvance;

    public Texture2D TextureCursorClose;

    public bool CursorChanged;

    [HideInInspector]
    public List<TextData> texts;
    [HideInInspector]
    public int currentText;
    [HideInInspector]
    public Image characterImage;
    public Text canvasText;
    public Image panelImage;
    public GameObject personagens;
    public GameObject gui;
    [HideInInspector]
    public Text charName;

    //Button ui

    public Sprite buttonUp;
    public Sprite buttonDown;
    public Font font;

    //Efeito de datilografia
    int charactersShown = 0;
    public float charDelay;
    float charTimer = 0;
    bool canProceed;
    bool isTalking;

    Color canvasColor = new Color(1, 1, 1, 1);

    GameObject buttonCanvas;

    void Start()
    {
        charTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Faz o fade in
        canvasColor.a = canvasColor.a + (1 - canvasColor.a) / 10f;

        panelImage.color = canvasColor;
        canvasText.color = canvasColor;

        //Faz a animação de texto


        if(isTalking){
            if(Input.GetMouseButton(0)){
                charTimer += Time.deltaTime * 5; //Acelera o texto
            }
            else{
                charTimer += Time.deltaTime;
            }

            if(charTimer > charDelay / 1000f){
                charTimer -= charDelay / 1000f;
                charactersShown ++;
                charactersShown = Mathf.Min(charactersShown, texts[currentText].texto.Length);
                canProceed = charactersShown == texts[currentText].texto.Length;
            }

            canvasText.text = texts[currentText].texto.Substring(0, charactersShown);

            if(canProceed && !CursorChanged){
                if(currentText+1 >= texts.Count){
                    EndCursor();
                }
                else{
                    ChangeCursor();
                }
            }
            if(Input.GetMouseButtonDown(0) && canProceed){
                if(BeginText(currentText + 1)){
                   CloseDialog();
                }
            }

            if(Input.GetMouseButtonDown(1)){
                charactersShown = texts[currentText].texto.Length;
            }
        }

        

    }

    public void OpenCharacterDialog(SpeechableCharacter personagem){
<<<<<<< HEAD
        Cursor.SetCursor(null, new Vector2(), CursorMode.Auto);

=======
        Cursor.SetCursor(null,hotSpot,CursorMode);
>>>>>>> ccabfbc0dfcb964e3a34a8441e237b22df95bebf
        gameObject.SetActive(true);
        personagens.SetActive(false);
        gui.SetActive(false);
        characterImage.sprite = manager.textureManager.GetSpritePose(personagem.data.defaultImage);
        characterImage.color = new Color(1, 1, 1, 1);
        canvasText.text = "";
        charName.text = "Detetive";
        //Abre o menu de botões

        if(buttonCanvas == null){

            //Cria o grupo de botões de perguntas
            buttonCanvas = new GameObject("Buttons Canvas");
            RectTransform rect = buttonCanvas.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(.5f, .5f);
            rect.anchorMax = new Vector2(.5f, .5f);

            List<Dialogo> digs = personagem.AvailableDialogs();

            float margin = (digs.Count + 1) * 10;

            rect.offsetMin = new Vector2(-200, -15 * digs.Count - margin/2f);
            rect.offsetMax = new Vector2(200, 15 * digs.Count + margin/2f);

            for(int i = 0; i < digs.Count; i++){


                //Cria cada botão
                Dialogo dig = digs[i];

                GameObject btn = new GameObject("Button " + i);
                RectTransform btnRect = btn.AddComponent<RectTransform>();
                
                btnRect.anchorMin = new Vector2(0, 1);
                btnRect.anchorMax = new Vector2(1, 1);
                btnRect.pivot = new Vector2(.5f, 1);
                btnRect.offsetMin = new Vector2(0, -40 -40*i);
                btnRect.offsetMax = new Vector2(0, -10 -40*i);

                Button btn_btn = btn.AddComponent<Button>();
                Image img = btn.AddComponent<Image>();
                btn_btn.targetGraphic = img;

                img.color = dig.read ? Color.gray : Color.white;

                img.sprite = buttonUp;
                img.type = Image.Type.Sliced;
                btn_btn.transition = Selectable.Transition.SpriteSwap;
                SpriteState ss = new SpriteState();
                ss.pressedSprite = buttonDown;
                btn_btn.spriteState = ss;

                btn.transform.SetParent(buttonCanvas.transform, false);

                btn_btn.onClick.AddListener( () => {
                    GlobalProfile.getInstance().SendMessage(dig.message);
                    OpenText(dig.texts);
                    dig.read = true;
                    manager.RefreshAllCharacterDialogs();
                });

                //Cria o texto do botão
                GameObject btnText = new GameObject();
                RectTransform txtRect = btnText.AddComponent<RectTransform>();

                txtRect.anchorMin = new Vector2(0, 0);
                txtRect.anchorMax = new Vector2(1, 1);

                txtRect.offsetMin = new Vector2(0, 0);
                txtRect.offsetMax = new Vector2(0, 0);

                Text txt = btnText.AddComponent<Text>();
                txt.alignment = TextAnchor.MiddleCenter;
                txt.text = dig.pergunta;
                txt.color = new Color(0, 0, 0);
                txt.font = font;
                btnText.transform.SetParent(btn.transform, false);

            }

            buttonCanvas.transform.SetParent(panelImage.gameObject.transform, false);
        }
        
    }

    public void OpenText(List<TextData> texts){
        gui.SetActive(false);
        Destroy(buttonCanvas);
        buttonCanvas = null;
        this.texts = texts;
        gameObject.SetActive(true);
        isTalking = true;
        BeginText(0);
    }

    public bool BeginText(int textIndex){        
        if(textIndex >= texts.Count) return true;
        CursorChanged = false;
        Cursor.SetCursor(TextureCursor,hotSpot,CursorMode);
        currentText = textIndex;
        canvasText.text = "";//texts[currentText];
        charTimer = 0;
        charactersShown = 0;
        canProceed = false;
        charName.text = texts[currentText].owner;
        Sprite image = manager.textureManager.GetSpritePose(texts[currentText].image);
        if(image != null){
            characterImage.sprite = image;
            characterImage.color = new Color(1, 1, 1, 1);
        }
        else{
            characterImage.color = new Color(1, 1, 1, 0);
        }
        return false;
    }


    public void CloseDialog(){
        gameObject.SetActive(false);
        personagens.SetActive(true);
        gui.SetActive(true);
        isTalking = false;
        Destroy(buttonCanvas);
        buttonCanvas = null;
        Cursor.SetCursor(null,hotSpot,CursorMode);
    }
    public void ChangeCursor(){
        Cursor.SetCursor(TextureCursorAdvance,hotSpot,CursorMode);
        CursorChanged = true;
    }
    public void EndCursor(){
        Cursor.SetCursor(TextureCursorClose,hotSpot,CursorMode);
        CursorChanged = true;
    }
}