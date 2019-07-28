using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechManager : MonoBehaviour
{
    // Start is called before the first frame update

    public TextData[] texts;
    public int currentText;
    public Image characterImage;
    public Text canvasText;
    public Image panelImage;

    //Efeito de datilografia

    int charactersShown = 0;
    public float charDelay;
    float charTimer = 0;
    bool canProceed;


    Color canvasColor = new Color(1, 1, 1, 1);


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

        if(Input.GetMouseButton(0)){
            charTimer += Time.deltaTime * 5;
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

        if(Input.GetMouseButtonDown(0) && canProceed){
            if(BeginText(currentText + 1)){
                gameObject.SetActive(false);
            }
        }

        

    }

    public void OpenText(TextData[] texts){
            this.texts = texts;
            gameObject.SetActive(true);
            RefreshGUI();
    }

    public bool BeginText(int textIndex){
        if(textIndex >= texts.Length) return true;
        currentText = textIndex;
        canvasText.text = "";//texts[currentText];
        charTimer = 0;
        charactersShown = 0;
        canProceed = false;
        if(texts[currentText].image != null){
            characterImage.sprite = texts[currentText].image;
            characterImage.color = new Color(1, 1, 1, 1);
        }
        else{
            characterImage.color = new Color(1, 1, 1, 0);
        }
        return false;
    }

    public void RefreshGUI(){
        BeginText(0);
        canvasColor.a = 0;
    }
}