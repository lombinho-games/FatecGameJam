using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LupaButton : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector]
    public bool pressed = false;
    public GameObject lupaInGame;
    Image lupaIcon;
    void Start()
    {
        lupaIcon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickOnButton(){

        pressed = !pressed;
        lupaInGame.SetActive(pressed);
        //Cursor.visible = false;
        Color temp = lupaIcon.color;

        temp.a = pressed ? 0 : 1;
        lupaIcon.color = temp;
    }
}
