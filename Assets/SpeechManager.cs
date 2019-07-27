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

    public Image canvasImage;
    public Text canvasText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshGUI(){
        currentText = 0;
        canvasImage.sprite = currentCharacterImage;
        canvasText.text = texts[currentText];
    }
}
