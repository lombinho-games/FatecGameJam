using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechableCharacter : MonoBehaviour
{
    public Canvas speechCanvas;
    public InspectionManager manager;

    [TextArea(3,10)]
    public string[] texts;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectCharacter(){
        if(!speechCanvas.gameObject.activeInHierarchy){
            Debug.Log("Clicou no fera");
            //Setar os valores do canvas
            SpeechManager sm = speechCanvas.GetComponent<SpeechManager>();

            sm.currentCharacterImage = spriteRenderer.sprite;
            sm.texts = texts;

            speechCanvas.gameObject.SetActive(true);

            sm.RefreshGUI();
        }
    }
}
