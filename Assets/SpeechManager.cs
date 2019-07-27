using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeechManager : MonoBehaviour
{
    // Start is called before the first frame update

    public string[] texts;
    public int currentText;
    public Sprite currentCharacterImage;

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

        if(currentCharacterImage != null)
            characterImage.color = canvasColor;
        else{
            characterImage.color = new Color(1, 1, 1, 0);
        }

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
            charactersShown = Mathf.Min(charactersShown, texts[currentText].Length);
            canProceed = charactersShown == texts[currentText].Length;
        }

        canvasText.text = texts[currentText].Substring(0, charactersShown);

        if(Input.GetMouseButtonDown(0) && canProceed){
            if(BeginText(currentText + 1)){
                gameObject.SetActive(false);
            }
        }

        

    }

    public void OpenText(Sprite image, string[] texts){
            currentCharacterImage = image;
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
        return false;
    }

    public void RefreshGUI(){
        if(currentCharacterImage != null)
            characterImage.sprite = currentCharacterImage;
        BeginText(0);
        canvasColor.a = 0;
    }
}
